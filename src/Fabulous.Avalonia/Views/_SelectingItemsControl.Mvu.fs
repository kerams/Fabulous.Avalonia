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
type MvuSelectingItemsControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SelectionChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedIndexChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<int, int>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedIndexChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedChangedInit: bool

    static member SelectionChanged =
        if not MvuSelectingItemsControl._SelectionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSelectingItemsControl._SelectionChangedInit then
                    MvuSelectingItemsControl._SelectionChanged <-
                        Attributes.Mvu.defineEvent<SelectionChangedEventArgs> "SelectingItemsControl_SelectionChanged" (fun target ->
                            (target :?> SelectingItemsControl).SelectionChanged)

                    MvuSelectingItemsControl._SelectionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSelectingItemsControl._SelectionChanged

    static member SelectedIndexChanged =
        if not MvuSelectingItemsControl._SelectedIndexChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSelectingItemsControl._SelectedIndexChangedInit then
                    MvuSelectingItemsControl._SelectedIndexChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "SelectingItemsControl_SelectedIndexChanged" SelectingItemsControl.SelectedIndexProperty

                    MvuSelectingItemsControl._SelectedIndexChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSelectingItemsControl._SelectedIndexChanged

    static member SelectedChanged =
        if not MvuSelectingItemsControl._SelectedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSelectingItemsControl._SelectedChangedInit then
                    MvuSelectingItemsControl._SelectedChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "SelectingItemsControl_SelectedChanged" SelectingItemsControl.IsSelectedProperty

                    MvuSelectingItemsControl._SelectedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSelectingItemsControl._SelectedChanged

type MvuSelectingItemsControlModifiers =
    /// <summary>Listens to the SelectingItemsControl SelectionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control's selection changes.</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabSelectingItemsControl>, fn: SelectionChangedEventArgs -> 'msg) =
        this.AddScalar(MvuSelectingItemsControl.SelectionChanged.WithValue(fn))

    /// <summary>Listens to the SelectingItemsControl SelectedIndexChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="index">Selected index</param>
    /// <param name="fn">Raised when the control's selected index changes.</param>
    [<Extension>]
    static member inline onSelectedIndexChanged(this: WidgetBuilder<'msg, #IFabSelectingItemsControl>, index: int, fn: int -> 'msg) =
        this.AddScalar(MvuSelectingItemsControl.SelectedIndexChanged.WithValue(ValueEventData.create index fn))

type MvuSelectingItemsControlAttachedModifiers =
    /// <summary>Listens to the SelectingItemsControl SelectedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Selected value</param>
    /// <param name="fn">Raised when the control's selected value changes.</param>
    [<Extension>]
    static member inline onSelectedChanged(this: WidgetBuilder<'msg, #IFabControl>, value: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuSelectingItemsControl.SelectedChanged.WithValue(ValueEventData.create value fn))
