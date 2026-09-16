namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Input
open Avalonia.Input.TextInput
open Avalonia.Interactivity
open Fabulous
open Fabulous.ScalarAttributeDefinitions

// Definitions are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps the
// definitions whose property the app reads. Registered keys always carry a kind bit, so 0 means not created yet.
[<AbstractClass; Sealed>]
type ComponentInputElement =
    [<DefaultValue>]
    static val mutable private keyDown: SimpleScalarAttributeDefinition<KeyEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private keyUp: SimpleScalarAttributeDefinition<KeyEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private textInput: SimpleScalarAttributeDefinition<TextInputEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private textInputMethodClientRequested: SimpleScalarAttributeDefinition<TextInputMethodClientRequestedEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerEntered: SimpleScalarAttributeDefinition<PointerEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerExited: SimpleScalarAttributeDefinition<PointerEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerMoved: SimpleScalarAttributeDefinition<PointerEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerPressed: SimpleScalarAttributeDefinition<PointerPressedEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerReleased: SimpleScalarAttributeDefinition<PointerReleasedEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerCaptureLost: SimpleScalarAttributeDefinition<PointerCaptureLostEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private pointerWheelChanged: SimpleScalarAttributeDefinition<PointerWheelEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private tapped: SimpleScalarAttributeDefinition<TappedEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private holding: SimpleScalarAttributeDefinition<HoldingRoutedEventArgs -> unit>

    [<DefaultValue>]
    static val mutable private doubleTapped: SimpleScalarAttributeDefinition<TappedEventArgs -> unit>

    static member KeyDown =
        if int ComponentInputElement.keyDown.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.keyDown.Key = 0 then
                    ComponentInputElement.keyDown <-
                        Attributes.Component.defineEvent<KeyEventArgs> "InputElement_KeyDown" (fun target -> (target :?> InputElement).KeyDown)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.keyDown

    static member KeyUp =
        if int ComponentInputElement.keyUp.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.keyUp.Key = 0 then
                    ComponentInputElement.keyUp <-
                        Attributes.Component.defineEvent<KeyEventArgs> "InputElement_KeyUp" (fun target -> (target :?> InputElement).KeyUp)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.keyUp

    static member TextInput =
        if int ComponentInputElement.textInput.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.textInput.Key = 0 then
                    ComponentInputElement.textInput <-
                        Attributes.Component.defineEvent<TextInputEventArgs> "InputElement_TextInput" (fun target -> (target :?> InputElement).TextInput)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.textInput

    static member TextInputMethodClientRequested =
        if int ComponentInputElement.textInputMethodClientRequested.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.textInputMethodClientRequested.Key = 0 then
                    ComponentInputElement.textInputMethodClientRequested <-
                        Attributes.Component.defineEvent<TextInputMethodClientRequestedEventArgs> "InputElement_TextInputMethodClientRequested" (fun target ->
                            (target :?> InputElement).TextInputMethodClientRequested)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.textInputMethodClientRequested

    static member PointerEntered =
        if int ComponentInputElement.pointerEntered.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerEntered.Key = 0 then
                    ComponentInputElement.pointerEntered <-
                        Attributes.Component.defineEvent<PointerEventArgs> "InputElement_PointerEntered" (fun target -> (target :?> InputElement).PointerEntered)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerEntered

    static member PointerExited =
        if int ComponentInputElement.pointerExited.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerExited.Key = 0 then
                    ComponentInputElement.pointerExited <-
                        Attributes.Component.defineEvent<PointerEventArgs> "InputElement_PointerExited" (fun target -> (target :?> InputElement).PointerExited)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerExited

    static member PointerMoved =
        if int ComponentInputElement.pointerMoved.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerMoved.Key = 0 then
                    ComponentInputElement.pointerMoved <-
                        Attributes.Component.defineEvent<PointerEventArgs> "InputElement_PointerMoved" (fun target -> (target :?> InputElement).PointerMoved)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerMoved

    static member PointerPressed =
        if int ComponentInputElement.pointerPressed.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerPressed.Key = 0 then
                    ComponentInputElement.pointerPressed <-
                        Attributes.Component.defineEvent<PointerPressedEventArgs> "InputElement_PointerPressed" (fun target -> (target :?> InputElement).PointerPressed)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerPressed

    static member PointerReleased =
        if int ComponentInputElement.pointerReleased.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerReleased.Key = 0 then
                    ComponentInputElement.pointerReleased <-
                        Attributes.Component.defineEvent<PointerReleasedEventArgs> "InputElement_PointerReleased" (fun target -> (target :?> InputElement).PointerReleased)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerReleased

    static member PointerCaptureLost =
        if int ComponentInputElement.pointerCaptureLost.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerCaptureLost.Key = 0 then
                    ComponentInputElement.pointerCaptureLost <-
                        Attributes.Component.defineEvent<PointerCaptureLostEventArgs> "InputElement_PointerCaptureLost" (fun target ->
                            (target :?> InputElement).PointerCaptureLost)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerCaptureLost

    static member PointerWheelChanged =
        if int ComponentInputElement.pointerWheelChanged.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.pointerWheelChanged.Key = 0 then
                    ComponentInputElement.pointerWheelChanged <-
                        Attributes.Component.defineEvent<PointerWheelEventArgs> "InputElement_PointerWheelChanged" (fun target -> (target :?> InputElement).PointerWheelChanged)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.pointerWheelChanged

    static member Tapped =
        if int ComponentInputElement.tapped.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.tapped.Key = 0 then
                    ComponentInputElement.tapped <-
                        Attributes.Component.defineEvent<TappedEventArgs> "InputElement_Tapped" (fun target -> (target :?> InputElement).Tapped)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.tapped

    static member Holding =
        if int ComponentInputElement.holding.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.holding.Key = 0 then
                    ComponentInputElement.holding <-
                        Attributes.Component.defineEvent<HoldingRoutedEventArgs> "InputElement_Holding" (fun target -> (target :?> InputElement).Holding)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.holding

    static member DoubleTapped =
        if int ComponentInputElement.doubleTapped.Key = 0 then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if int ComponentInputElement.doubleTapped.Key = 0 then
                    ComponentInputElement.doubleTapped <-
                        Attributes.Component.defineEvent<TappedEventArgs> "InputElement_DoubleTapped" (fun target -> (target :?> InputElement).DoubleTapped)
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentInputElement.doubleTapped

type ComponentInputElementModifiers =

    /// <summary>Listens to the InputElement KeyDown event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is pressed while the control has focus.</param>
    [<Extension>]
    static member inline onKeyDown(this: WidgetBuilder<'msg, #IFabInputElement>, fn: KeyEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.KeyDown.WithValue(fn))

    /// <summary>Listens to the InputElement KeyUp event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is released while the control has focus.</param>
    [<Extension>]
    static member inline onKeyUp(this: WidgetBuilder<'msg, #IFabInputElement>, fn: KeyEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.KeyUp.WithValue(fn))

    /// <summary>Listens to the InputElement TextInput event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a user typed some text while the control has focus.</param>
    [<Extension>]
    static member inline onTextInput(this: WidgetBuilder<'msg, #IFabInputElement>, fn: TextInputEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.TextInput.WithValue(fn))

    /// <summary>Listens to the InputElement TextInputMethodClientRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when an input element gains input focus and input method is looking for the corresponding client.</param>
    [<Extension>]
    static member inline onTextInputMethodClientRequested(this: WidgetBuilder<'msg, #IFabInputElement>, fn: TextInputMethodClientRequestedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.TextInputMethodClientRequested.WithValue(fn))

    /// <summary>Listens to the InputElement PointerEntered event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer enters the control.</param>
    [<Extension>]
    static member inline onPointerEntered(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerEntered.WithValue(fn))

    /// <summary>Listens to the InputElement PointerExited event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer leaves the control.</param>
    [<Extension>]
    static member inline onPointerExited(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerExited.WithValue(fn))

    /// <summary>Listens to the InputElement PointerMoved event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer moves over the control.</param>
    [<Extension>]
    static member inline onPointerMoved(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerMoved.WithValue(fn))

    /// <summary>Listens to the InputElement PointerPressed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is pressed over the control.</param>
    [<Extension>]
    static member inline onPointerPressed(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerPressedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerPressed.WithValue(fn))

    /// <summary>Listens to the InputElement PointerReleased event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is released over the control.</param>
    [<Extension>]
    static member inline onPointerReleased(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerReleasedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerReleased.WithValue(fn))

    /// <summary>Listens to the InputElement PointerCaptureLost event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control or its child control loses the pointer capture for any reason event will not be triggered for a parent control if capture was transferred to another child of that parent control.</param>
    [<Extension>]
    static member inline onPointerCaptureLost(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerCaptureLostEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerCaptureLost.WithValue(fn))

    /// <summary>Listens to the InputElement PointerWheelChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer wheel changes.</param>
    [<Extension>]
    static member inline onPointerWheelChanged(this: WidgetBuilder<'msg, #IFabInputElement>, fn: PointerWheelEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.PointerWheelChanged.WithValue(fn))

    /// <summary>Listens to the InputElement Tapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<'msg, #IFabInputElement>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.Tapped.WithValue(fn))

    /// <summary>Listens to the InputElement Holding event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a holding gesture occurs on the control.</param>
    [<Extension>]
    static member inline onHolding(this: WidgetBuilder<'msg, #IFabInputElement>, fn: HoldingRoutedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.Holding.WithValue(fn))

    /// <summary>Listens to the InputElement RightTapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a double-tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onDoubleTapped(this: WidgetBuilder<'msg, #IFabInputElement>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentInputElement.DoubleTapped.WithValue(fn))
