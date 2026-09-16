namespace Fabulous.Avalonia

open System
open System.IO
open Avalonia
open Avalonia.Controls
open Avalonia.Interactivity
open Avalonia.Media.Imaging
open Fabulous
open Fabulous.ScalarAttributeDefinitions

[<RequireQualifiedAccess; NoComparison>]
type ImageSourceValue =
    | Bitmap of source: Bitmap
    | File of source: string
    | Uri of source: Uri
    | Stream of source: Stream

[<RequireQualifiedAccess>]
module ScalarAttributeComparers =
    let inline physicalEqualityCompare a b =
        if LanguagePrimitives.PhysicalEquality a b then
            ScalarAttributeComparison.Identical
        else
            ScalarAttributeComparison.Different

// Plain structs rather than records holding 'data voption, for the same NativeAOT size reason as ScalarValue.
[<Struct; NoEquality; NoComparison>]
type ComponentValueEventData<'data, 'eventArgs> =
    val Value: ScalarValue<'data>
    val Event: 'eventArgs -> unit
    new(value, event) = { Value = value; Event = event }

module ComponentValueEventData =
    let create (value: 'data) (event: 'eventArgs -> unit) =
        ComponentValueEventData(ScalarValue(value), event)

    let createOptional (value: ScalarValue<'data>) (event: 'eventArgs -> unit) = ComponentValueEventData(value, event)

[<Struct; NoEquality; NoComparison>]
type ValueEventData<'data, 'eventArgs> =
    val Value: ScalarValue<'data>
    val Event: 'eventArgs -> MsgValue
    new(value, event) = { Value = value; Event = event }

module ValueEventData =
    let create (value: 'data) (event: 'eventArgs -> 'msg) =
        ValueEventData(ScalarValue(value), event >> box >> MsgValue)

    let createOptional (value: ScalarValue<'data>) (event: 'eventArgs -> 'msg) =
        ValueEventData(value, event >> box >> MsgValue)

// Dedicated attribute data classes for the definitions instantiated over many types (see ScalarAttributeData in Fabulous)

[<Sealed>]
type AvaloniaPropertyData<'modelType, 'valueType>
    (
        property: AvaloniaProperty<'valueType>,
        convertValue: 'modelType -> 'valueType,
        compare: 'modelType -> 'modelType -> ScalarAttributeComparison
    ) =
    inherit ScalarAttributeData()

    override _.CompareBoxed(a, b) =
        compare (unbox<'modelType> a) (unbox<'modelType> b)

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> target.SetValue(property, convertValue(unbox<'modelType> v)) |> ignore
        | ValueNone -> target.ClearValue(property)

[<Sealed>]
type AvaloniaPropertyWithEqualityData<'T when 'T: equality>(property: AvaloniaProperty<'T>) =
    inherit ScalarAttributeData()

    override _.CompareBoxed(a, b) =
        ScalarAttributeComparers.equalityCompare (unbox<'T> a) (unbox<'T> b)

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> target.SetValue(property, unbox<'T> v) |> ignore
        | ValueNone -> target.ClearValue(property)

/// bool/float/int/enum properties: the value travels in ScalarAttribute.NumericValue, so it is never boxed and the diff
/// compares it as bits without calling the definition
[<Sealed>]
type AvaloniaPropertySmallData<'T>(property: AvaloniaProperty<'T>, decode: uint64 -> 'T) =
    inherit SmallScalarAttributeData()

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> target.SetValue(property, decode v) |> ignore
        | ValueNone -> target.ClearValue(property)

[<Sealed>]
type AvaloniaPropertyConverterData<'T, 'modelType, 'valueType>(property: AvaloniaProperty<'T>, convert: 'modelType -> 'valueType) =
    inherit ScalarAttributeData()

    override _.CompareBoxed(_, _) = ScalarAttributeComparison.Different

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> target.SetValue(property, convert(unbox<'modelType> v)) |> ignore
        | ValueNone -> target.ClearValue(property)

[<Sealed>]
type PropertyWithDefaultData<'T when 'T: equality>(defaultValue: 'T, setter: obj -> 'T -> unit) =
    inherit ScalarAttributeData()

    override _.CompareBoxed(a, b) =
        ScalarAttributeComparers.equalityCompare (unbox<'T> a) (unbox<'T> b)

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> setter target (unbox<'T> v)
        | ValueNone -> setter target defaultValue

[<Sealed>]
type PropertyWithGetSetData<'T when 'T: equality>(getter: obj -> 'T, setter: obj -> 'T -> unit) =
    inherit ScalarAttributeData()

    override _.CompareBoxed(a, b) =
        ScalarAttributeComparers.equalityCompare (unbox<'T> a) (unbox<'T> b)

    override _.UpdateNode(_, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueSome v -> setter target (unbox<'T> v)
        | ValueNone -> setter target (getter target)

/// Sets the property from the attribute value and reports its changes through the attribute's event
[<AbstractClass>]
type AvaloniaPropertyChangedEventData<'valueType>(property: AvaloniaProperty<'valueType>) =
    inherit ScalarAttributeData()

    /// Sets the property from the attribute value, if it carries one
    abstract SetValue: value: obj * target: AvaloniaObject -> unit

    /// Reports a change of the property to the event of the latest attribute value (slot.Handler)
    abstract Report: slot: EventHandlerSlot * newValue: 'valueType * node: IViewNode -> unit

    override _.CompareBoxed(_, _) = ScalarAttributeComparison.Different

    override this.UpdateNode(oldValue, newValue, node) =
        let target = node.Target :?> AvaloniaObject

        match newValue with
        | ValueNone ->
            // The attribute is no longer applied, so we clean up the event
            node.RemoveHandler(property.Name)

            if oldValue.IsSome then
                target.ClearValue(property)
        | ValueSome value ->
            match EventHandlerSlot.tryGet node property.Name with
            | null ->
                // subscribe after setting the value, so the initial value is not reported as a change
                this.SetValue(value, target)

                EventHandlerSlot.set node property.Name value (fun slot ->
                    property.Changed.Subscribe(fun args ->
                        if not slot.Suppressed && args.Sender = target && args.NewValue.HasValue then
                            this.Report(slot, args.NewValue.Value, node)))
            | slot ->
                slot.Handler <- value
                // the subscription stays in place, so the change caused by applying the value must not be reported
                slot.Suppressed <- true

                try
                    this.SetValue(value, target)
                finally
                    slot.Suppressed <- false

[<Sealed>]
type MvuAvaloniaPropertyChangedEventData<'modelType, 'valueType>
    (property: AvaloniaProperty<'valueType>, convertToValue: 'modelType -> 'valueType, convertToModel: 'valueType -> 'modelType) =
    inherit AvaloniaPropertyChangedEventData<'valueType>(property)

    override _.SetValue(value, target) =
        let curr = unbox<ValueEventData<'modelType, 'modelType>> value

        if curr.Value.HasValue then
            target.SetValue(property, box(convertToValue curr.Value.Value)) |> ignore

    override _.Report(slot, newValue, node) =
        let curr = unbox<ValueEventData<'modelType, 'modelType>> slot.Handler
        let (MsgValue r) = curr.Event(convertToModel newValue)
        Dispatcher.dispatch node r

[<Sealed>]
type ComponentAvaloniaPropertyChangedEventData<'modelType, 'valueType>
    (property: AvaloniaProperty<'valueType>, convertToValue: 'modelType -> 'valueType, convertToModel: 'valueType -> 'modelType) =
    inherit AvaloniaPropertyChangedEventData<'valueType>(property)

    override _.SetValue(value, target) =
        let curr = unbox<ComponentValueEventData<'modelType, 'modelType>> value

        if curr.Value.HasValue then
            target.SetValue(property, box(convertToValue curr.Value.Value)) |> ignore

    override _.Report(slot, newValue, _) =
        let curr = unbox<ComponentValueEventData<'modelType, 'modelType>> slot.Handler
        curr.Event(convertToModel newValue)

/// Routed and plain event handlers; unlike the Fabulous event data, removing one disposes the whole node
[<AbstractClass>]
type AvaloniaEventData(name: string) =
    inherit ScalarAttributeData()

    /// Subscribes to the event, invoking slot.Handler (read on every event, not captured) when it fires
    abstract Subscribe: slot: EventHandlerSlot * node: IViewNode -> IDisposable

    override _.CompareBoxed(_, _) = ScalarAttributeComparison.Different

    override this.UpdateNode(_, newValue, node) =
        match newValue with
        | ValueNone ->
            match node.TryGetHandler(name) with
            | null -> ()
            | handler -> handler.Dispose()

            node.Dispose()
        | ValueSome handler -> EventHandlerSlot.set node name handler (fun slot -> this.Subscribe(slot, node))

[<Sealed>]
type MvuRoutedEventData<'args when 'args :> RoutedEventArgs>(name: string, property: RoutedEvent<'args>) =
    inherit AvaloniaEventData(name)

    override _.Subscribe(slot, node) =
        property.AddClassHandler(fun _ args ->
            let (MsgValue r) = (unbox<'args -> MsgValue> slot.Handler) args
            Dispatcher.dispatch node r)

[<Sealed>]
type ComponentRoutedEventData<'args when 'args :> RoutedEventArgs>(name: string, property: RoutedEvent<'args>) =
    inherit AvaloniaEventData(name)

    override _.Subscribe(slot, _) =
        property.AddClassHandler(fun _ args -> (unbox<'args -> unit> slot.Handler) args)

[<Sealed>]
type MvuEventHandlerData<'handler, 'args when 'handler :> Delegate and 'handler: delegate<'args, unit>>
    (name: string, getEvent: obj -> IEvent<'handler, 'args>) =
    inherit AvaloniaEventData(name)

    override _.Subscribe(slot, node) =
        (getEvent node.Target)
            .Subscribe(fun args ->
                let (MsgValue r) = (unbox<'args -> MsgValue> slot.Handler) args
                Dispatcher.dispatch node r)

[<Sealed>]
type ComponentEventHandlerData<'handler, 'args when 'handler :> Delegate and 'handler: delegate<'args, unit>>
    (name: string, getEvent: obj -> IEvent<'handler, 'args>) =
    inherit AvaloniaEventData(name)

    override _.Subscribe(slot, node) =
        (getEvent node.Target).Subscribe(fun args -> (unbox<'args -> unit> slot.Handler) args)

module Attributes =
    /// Define an attribute for an AvaloniaProperty
    let defineAvaloniaProperty<'modelType, 'valueType>
        (property: AvaloniaProperty<'valueType>)
        (convertValue: 'modelType -> 'valueType)
        (compare: 'modelType -> 'modelType -> ScalarAttributeComparison)
        : ScalarAttributeDefinition<'modelType, 'valueType> =
        { Key = AttributeDefinitionStore.registerScalar(AvaloniaPropertyData<'modelType, 'valueType>(property, convertValue, compare))
          Name = property.Name }

    /// Define an attribute for an AvaloniaProperty supporting equality comparison
    let defineAvaloniaPropertyWithEquality<'T when 'T: equality> (directProperty: AvaloniaProperty<'T>) : SimpleScalarAttributeDefinition<'T> =
        { Key = AttributeDefinitionStore.registerScalar(AvaloniaPropertyWithEqualityData<'T>(directProperty))
          Name = directProperty.Name }

    /// Define an attribute for a bool AvaloniaProperty, stored unboxed
    let defineAvaloniaPropertyBool (property: AvaloniaProperty<bool>) : SmallScalarAttributeDefinition<bool> =
        { Key = AttributeDefinitionStore.registerSmallScalar(AvaloniaPropertySmallData<bool>(property, fun v -> SmallScalars.Bool.decode v))
          Name = property.Name }

    /// Define an attribute for a float AvaloniaProperty, stored unboxed
    let defineAvaloniaPropertyFloat (property: AvaloniaProperty<float>) : SmallScalarAttributeDefinition<float> =
        { Key = AttributeDefinitionStore.registerSmallScalar(AvaloniaPropertySmallData<float>(property, fun v -> SmallScalars.Float.decode v))
          Name = property.Name }

    /// Define an attribute for an int AvaloniaProperty, stored unboxed
    let defineAvaloniaPropertyInt (property: AvaloniaProperty<int>) : SmallScalarAttributeDefinition<int> =
        { Key = AttributeDefinitionStore.registerSmallScalar(AvaloniaPropertySmallData<int>(property, fun v -> SmallScalars.Int.decode v))
          Name = property.Name }

    /// Define an attribute for an int-backed enum AvaloniaProperty, stored unboxed
    let defineAvaloniaPropertyEnum<'T when 'T: enum<int> and 'T: struct> (property: AvaloniaProperty<'T>) : SmallScalarAttributeDefinition<'T> =
        let decode (v: uint64) =
            let mutable value = int v
            System.Runtime.CompilerServices.Unsafe.As<int, 'T>(&value)

        { Key = AttributeDefinitionStore.registerSmallScalar(AvaloniaPropertySmallData<'T>(property, decode))
          Name = property.Name }

    /// Define an attribute for an AvaloniaProperty supporting equality comparison with a default value and setter
    let defineProperty<'T when 'T: equality> name (defaultValue: 'T) (setter: obj -> 'T -> unit) : SimpleScalarAttributeDefinition<'T> =
        { Key = AttributeDefinitionStore.registerScalar(PropertyWithDefaultData<'T>(defaultValue, setter))
          Name = name }

    /// Define an attribute for an AvaloniaProperty supporting equality comparison with getter and setter
    let definePropertyWithGetSet<'T when 'T: equality> name (getter: obj -> 'T) (setter: obj -> 'T -> unit) : SimpleScalarAttributeDefinition<'T> =
        { Key = AttributeDefinitionStore.registerScalar(PropertyWithGetSetData<'T>(getter, setter))
          Name = name }

    /// Define an attribute for an AvaloniaProperty supporting equality comparison and converter
    let defineAvaloniaPropertyWithEqualityConverter<'T, 'modelType, 'valueType when 'T: equality>
        (directProperty: AvaloniaProperty<'T>)
        (convert: 'modelType -> 'valueType)
        : ScalarAttributeDefinition<'modelType, 'valueType> =
        { Key = AttributeDefinitionStore.registerScalar(AvaloniaPropertyConverterData<'T, 'modelType, 'valueType>(directProperty, convert))
          Name = directProperty.Name }

    /// Define an attribute storing a Widget for an AvaloniaProperty
    let defineAvaloniaPropertyWidget (property: AvaloniaProperty<'T | null>) =
        Attributes.definePropertyWidget property.Name (fun target -> (target :?> AvaloniaObject).GetValue(property)) (fun target value ->
            let avaloniaObject = target :?> AvaloniaObject

            if isNull value then
                avaloniaObject.ClearValue(property)
            else
                avaloniaObject.SetValue(property, value) |> ignore)


    /// Performance optimization: avoid allocating a new ImageSource instance on each update
    /// we store the user value (e.g. Bitmap, string, Uri, Stream) and convert it to an ImageSource only when needed
    let defineBindableImageSource (property: AvaloniaProperty) =
        Attributes.defineScalar<ImageSourceValue, ImageSourceValue> property.Name id ScalarAttributeComparers.equalityCompare (fun _ newValue node ->
            let target = node.Target :?> AvaloniaObject

            if not newValue.HasValue then
                target.ClearValue(property)
            else
                let value =
                    match newValue.Value with
                    | ImageSourceValue.Bitmap source -> source
                    | ImageSourceValue.File file -> ImageSource.fromString file
                    | ImageSourceValue.Uri uri -> ImageSource.fromUri uri
                    | ImageSourceValue.Stream stream -> ImageSource.fromStream(stream)

                target.SetValue(property, value) |> ignore)

    /// Performance optimization: avoid allocating a new WindowIcon instance on each update
    /// we store the user value (e.g. Bitmap, string, Uri, Stream) and convert it to an ImageSource only when needed
    let defineBindableWindowIconSource (property: AvaloniaProperty) =
        Attributes.defineScalar<ImageSourceValue, ImageSourceValue> property.Name id ScalarAttributeComparers.equalityCompare (fun _ newValue node ->
            let target = node.Target :?> AvaloniaObject

            if not newValue.HasValue then
                target.ClearValue(property)
            else
                let value =
                    match newValue.Value with
                    | ImageSourceValue.Bitmap source -> WindowIcon(source)
                    | ImageSourceValue.File file -> WindowIcon(ImageSource.fromString file)
                    | ImageSourceValue.Uri uri -> WindowIcon(ImageSource.fromUri uri)
                    | ImageSourceValue.Stream stream -> WindowIcon(ImageSource.fromStream(stream))

                target.SetValue(property, value) |> ignore)

    let defineAvaloniaNonGenericListWidgetCollection name (getCollection: obj -> System.Collections.IList) =
        let applyDiff _ (diffs: WidgetCollectionItemChanges) (node: IViewNode) =
            let targetColl = getCollection node.Target

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Remove(index, widget) ->
                    let itemNode = node.TreeContext.GetViewNode(targetColl[index])

                    // Trigger the unmounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Unmounted
                    itemNode.Dispose()

                    // Remove the child from the UI tree
                    targetColl.RemoveAt(index)

                | _ -> ()

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Insert(index, widget) ->
                    let struct (itemNode, view) = Helpers.createViewForWidget node &widget

                    // Insert the new child into the UI tree
                    targetColl.Insert(index, unbox view)

                    // Trigger the mounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Mounted

                | WidgetCollectionItemChange.Update(index, widgetDiff) ->
                    let childNode = node.TreeContext.GetViewNode(targetColl[index])

                    childNode.ApplyDiff(&widgetDiff)

                | WidgetCollectionItemChange.Replace(index, oldWidget, newWidget) ->
                    let prevItemNode = node.TreeContext.GetViewNode(targetColl[index])

                    let struct (nextItemNode, view) = Helpers.createViewForWidget node &newWidget

                    // Trigger the unmounted event for the old child
                    Dispatcher.dispatchEventForAllChildren prevItemNode &oldWidget Lifecycle.Unmounted
                    prevItemNode.Dispose()

                    // Replace the existing child in the UI tree at the index with the new one
                    targetColl[index] <- view

                    // Trigger the mounted event for the new child
                    Dispatcher.dispatchEventForAllChildren nextItemNode &newWidget Lifecycle.Mounted

                | _ -> ()

        let updateNode _ (newValueOpt: ArraySlice<Widget> voption) (node: IViewNode) =
            let targetColl = getCollection node.Target
            targetColl.Clear()

            match newValueOpt with
            | ValueNone -> ()
            | ValueSome widgets ->
                for widget in ArraySlice.toSpan widgets do
                    let struct (_, view) = Helpers.createViewForWidget node &widget

                    targetColl.Add(view) |> ignore

        Attributes.defineWidgetCollection name applyDiff updateNode

    /// Define an attribute storing a collection of Widget for a AvaloniaList<T> property
    let defineAvaloniaListWidgetCollection<'itemType> name (getCollection: obj -> System.Collections.Generic.IList<'itemType>) =
        let applyDiff _ (diffs: WidgetCollectionItemChanges) (node: IViewNode) =
            let targetColl = getCollection node.Target

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Remove(index, widget) ->
                    let itemNode = node.TreeContext.GetViewNode(box targetColl[index])

                    // Trigger the unmounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Unmounted
                    itemNode.Dispose()

                    // Remove the child from the UI tree
                    targetColl.RemoveAt(index)

                | _ -> ()

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Insert(index, widget) ->
                    let struct (itemNode, view) = Helpers.createViewForWidget node &widget

                    // Insert the new child into the UI tree
                    targetColl.Insert(index, unbox view)

                    // Trigger the mounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Mounted

                | WidgetCollectionItemChange.Update(index, widgetDiff) ->
                    let childNode = node.TreeContext.GetViewNode(box targetColl[index])

                    childNode.ApplyDiff(&widgetDiff)

                | WidgetCollectionItemChange.Replace(index, oldWidget, newWidget) ->
                    let prevItemNode = node.TreeContext.GetViewNode(box targetColl[index])

                    let struct (nextItemNode, view) = Helpers.createViewForWidget node &newWidget

                    // Trigger the unmounted event for the old child
                    Dispatcher.dispatchEventForAllChildren prevItemNode &oldWidget Lifecycle.Unmounted
                    prevItemNode.Dispose()

                    // Replace the existing child in the UI tree at the index with the new one
                    targetColl[index] <- unbox view

                    // Trigger the mounted event for the new child
                    Dispatcher.dispatchEventForAllChildren nextItemNode &newWidget Lifecycle.Mounted

                | _ -> ()

        let updateNode _ (newValueOpt: ArraySlice<Widget> voption) (node: IViewNode) =
            let targetColl = getCollection node.Target
            targetColl.Clear()

            match newValueOpt with
            | ValueNone -> ()
            | ValueSome widgets ->
                for widget in ArraySlice.toSpan widgets do
                    let struct (_, view) = Helpers.createViewForWidget node &widget

                    targetColl.Add(unbox view)

        Attributes.defineWidgetCollection name applyDiff updateNode

    let inline defineAvaloniaListWidgetCollectionWithCustomDiff<'itemType>
        name
        (getCollection: obj -> System.Collections.Generic.IList<'itemType>)
        ([<InlineIfLambda>] onInsert: obj -> int -> obj -> unit)
        ([<InlineIfLambda>] onRemove: obj -> int -> obj -> unit)
        ([<InlineIfLambda>] onReplace: obj -> int -> obj -> obj -> unit)
        =
        let applyDiff _ (diffs: WidgetCollectionItemChanges) (node: IViewNode) =
            let target = node.Target
            let targetColl = getCollection target

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Remove(index, widget) ->
                    let item = targetColl[index]
                    let itemNode = node.TreeContext.GetViewNode(box item)

                    // Trigger the unmounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Unmounted
                    itemNode.Dispose()

                    // Call custom remove handler
                    onRemove target index (box item)

                    // Remove from collection
                    targetColl.RemoveAt(index)

                | _ -> ()

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Insert(index, widget) ->
                    let struct (itemNode, view) = Helpers.createViewForWidget node &widget

                    // Call custom insert handler
                    onInsert target index view

                    // Insert into collection
                    targetColl.Insert(index, unbox view)

                    // Trigger the mounted event
                    Dispatcher.dispatchEventForAllChildren itemNode &widget Lifecycle.Mounted

                | WidgetCollectionItemChange.Update(index, widgetDiff) ->
                    let childNode = node.TreeContext.GetViewNode(box targetColl[index])
                    childNode.ApplyDiff(&widgetDiff)

                | WidgetCollectionItemChange.Replace(index, oldWidget, newWidget) ->
                    let oldItem = targetColl[index]
                    let prevItemNode = node.TreeContext.GetViewNode(box oldItem)
                    let struct (nextItemNode, view) = Helpers.createViewForWidget node &newWidget

                    // Trigger unmounted event
                    Dispatcher.dispatchEventForAllChildren prevItemNode &oldWidget Lifecycle.Unmounted
                    prevItemNode.Dispose()

                    // Call custom replace handler
                    onReplace target index (box oldItem) view

                    // Update collection
                    targetColl[index] <- unbox view

                    // Trigger mounted event
                    Dispatcher.dispatchEventForAllChildren nextItemNode &newWidget Lifecycle.Mounted

                | _ -> ()

        let updateNode _ (newValueOpt: ArraySlice<Widget> voption) (node: IViewNode) =
            let target = node.Target
            let targetColl = getCollection target

            // Remove all existing items
            for i = targetColl.Count - 1 downto 0 do
                let item = targetColl[i]
                onRemove target i (box item)

            targetColl.Clear()

            // Add new items
            match newValueOpt with
            | ValueNone -> ()
            | ValueSome widgets ->
                for widget in ArraySlice.toSpan widgets do
                    let struct (_, view) = Helpers.createViewForWidget node &widget
                    onInsert target widget.Key view
                    targetColl.Add(unbox view)

        Attributes.defineWidgetCollection name applyDiff updateNode


    module Mvu =
        let defineAvaloniaPropertyWithChangedEvent<'modelType, 'valueType>
            name
            (property: AvaloniaProperty<'valueType>)
            (convertToValue: 'modelType -> 'valueType)
            (convertToModel: 'valueType -> 'modelType)
            : SimpleScalarAttributeDefinition<ValueEventData<'modelType, 'modelType>> =
            { Key =
                AttributeDefinitionStore.registerScalar(
                    MvuAvaloniaPropertyChangedEventData<'modelType, 'valueType>(property, convertToValue, convertToModel)
                )
              Name = name }

        let defineAvaloniaPropertyWithChangedEvent'<'T> name (property: AvaloniaProperty<'T>) : SimpleScalarAttributeDefinition<ValueEventData<'T, 'T>> =
            defineAvaloniaPropertyWithChangedEvent<'T, 'T> name property id id

        let defineRoutedEvent<'args when 'args :> RoutedEventArgs> name (property: RoutedEvent<'args>) : SimpleScalarAttributeDefinition<'args -> MsgValue> =
            { Key = AttributeDefinitionStore.registerScalar(MvuRoutedEventData<'args>(name, property))
              Name = name }

        let defineEventHandler name (getEvent: obj -> IEvent<'handler, 'args>) : SimpleScalarAttributeDefinition<'args -> MsgValue> =
            { Key = AttributeDefinitionStore.registerScalar(MvuEventHandlerData<'handler, 'args>(name, getEvent))
              Name = name }

    module Component =

        let defineAvaloniaPropertyWithChangedEvent<'modelType, 'valueType>
            name
            (property: AvaloniaProperty<'valueType>)
            (convertToValue: 'modelType -> 'valueType)
            (convertToModel: 'valueType -> 'modelType)
            : SimpleScalarAttributeDefinition<ComponentValueEventData<'modelType, 'modelType>> =
            { Key =
                AttributeDefinitionStore.registerScalar(
                    ComponentAvaloniaPropertyChangedEventData<'modelType, 'valueType>(property, convertToValue, convertToModel)
                )
              Name = name }

        let defineAvaloniaPropertyWithChangedEvent'<'T>
            name
            (property: AvaloniaProperty<'T>)
            : SimpleScalarAttributeDefinition<ComponentValueEventData<'T, 'T>> =
            defineAvaloniaPropertyWithChangedEvent<'T, 'T> name property id id

        let defineRoutedEvent<'args when 'args :> RoutedEventArgs> name (property: RoutedEvent<'args>) : SimpleScalarAttributeDefinition<'args -> unit> =
            { Key = AttributeDefinitionStore.registerScalar(ComponentRoutedEventData<'args>(name, property))
              Name = name }

        let defineEventHandler name (getEvent: obj -> IEvent<'handler, 'args>) : SimpleScalarAttributeDefinition<'args -> unit> =
            { Key = AttributeDefinitionStore.registerScalar(ComponentEventHandlerData<'handler, 'args>(name, getEvent))
              Name = name }
