namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentVirtualizingStackPanel =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChangedInit: bool

    static member HorizontalSnapPointsChanged =
        if not ComponentVirtualizingStackPanel._HorizontalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentVirtualizingStackPanel._HorizontalSnapPointsChangedInit then
                    ComponentVirtualizingStackPanel._HorizontalSnapPointsChanged <-
                        Attributes.Component.defineEvent "VirtualizingStackPanel_HorizontalSnapPointsChanged" (fun target ->
                            (target :?> VirtualizingStackPanel)
                                .HorizontalSnapPointsChanged)

                    ComponentVirtualizingStackPanel._HorizontalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentVirtualizingStackPanel._HorizontalSnapPointsChanged

    static member VerticalSnapPointsChanged =
        if not ComponentVirtualizingStackPanel._VerticalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentVirtualizingStackPanel._VerticalSnapPointsChangedInit then
                    ComponentVirtualizingStackPanel._VerticalSnapPointsChanged <-
                        Attributes.Component.defineEvent "VirtualizingStackPanel_VerticalSnapPointsChanged" (fun target ->
                            (target :?> VirtualizingStackPanel)
                                .VerticalSnapPointsChanged)

                    ComponentVirtualizingStackPanel._VerticalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentVirtualizingStackPanel._VerticalSnapPointsChanged

type ComponentVirtualizingStackPanelModifiers =

    /// <summary>Listens to the StackPanel HorizontalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the HorizontalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onHorizontalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabVirtualizingStackPanel>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentVirtualizingStackPanel.HorizontalSnapPointsChanged.WithValue(fn))

    /// <summary>Listens to the StackPanel VerticalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the VerticalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onVerticalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabVirtualizingStackPanel>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentVirtualizingStackPanel.VerticalSnapPointsChanged.WithValue(fn))
