namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentSelectingItemsControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SelectionChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedIndexChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<int, int>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedIndexChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedChangedInit: bool

    static member SelectionChanged =
        if not ComponentSelectingItemsControl._SelectionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSelectingItemsControl._SelectionChangedInit then
                    ComponentSelectingItemsControl._SelectionChanged <-
                        Attributes.Component.defineEvent<SelectionChangedEventArgs> "SelectingItemsControl_SelectionChanged" (fun target ->
                            (target :?> SelectingItemsControl).SelectionChanged)

                    ComponentSelectingItemsControl._SelectionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSelectingItemsControl._SelectionChanged

    static member SelectedIndexChanged =
        if not ComponentSelectingItemsControl._SelectedIndexChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSelectingItemsControl._SelectedIndexChangedInit then
                    ComponentSelectingItemsControl._SelectedIndexChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "SelectingItemsControl_SelectedIndexChanged" SelectingItemsControl.SelectedIndexProperty

                    ComponentSelectingItemsControl._SelectedIndexChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSelectingItemsControl._SelectedIndexChanged

    static member SelectedChanged =
        if not ComponentSelectingItemsControl._SelectedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSelectingItemsControl._SelectedChangedInit then
                    ComponentSelectingItemsControl._SelectedChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "SelectingItemsControl_SelectedChanged" SelectingItemsControl.IsSelectedProperty

                    ComponentSelectingItemsControl._SelectedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSelectingItemsControl._SelectedChanged

type ComponentSelectingItemsControlModifiers =
    /// <summary>Listens to the SelectingItemsControl SelectionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control's selection changes.</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabSelectingItemsControl>, fn: SelectionChangedEventArgs -> unit) =
        this.AddScalar(ComponentSelectingItemsControl.SelectionChanged.WithValue(fn))

    /// <summary>Listens to the SelectingItemsControl SelectedIndexChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="index">Selected index</param>
    /// <param name="fn">Raised when the control's selected index changes.</param>
    [<Extension>]
    static member inline onSelectedIndexChanged(this: WidgetBuilder<'msg, #IFabSelectingItemsControl>, index: int, fn: int -> unit) =
        this.AddScalar(ComponentSelectingItemsControl.SelectedIndexChanged.WithValue(ComponentValueEventData.create index fn))

type ComponentSelectingItemsControlAttachedModifiers =
    /// <summary>Listens to the SelectingItemsControl SelectedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Selected value</param>
    /// <param name="fn">Raised when the control's selected value changes.</param>
    [<Extension>]
    static member inline onSelectedChanged(this: WidgetBuilder<'msg, #IFabControl>, value: bool, fn: bool -> unit) =
        this.AddScalar(ComponentSelectingItemsControl.SelectedChanged.WithValue(ComponentValueEventData.create value fn))
