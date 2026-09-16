namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentStackPanel =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChangedInit: bool

    static member HorizontalSnapPointsChanged =
        if not ComponentStackPanel._HorizontalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentStackPanel._HorizontalSnapPointsChangedInit then
                    ComponentStackPanel._HorizontalSnapPointsChanged <-
                        Attributes.Component.defineEvent "StackPanel_HorizontalSnapPointsChanged" (fun target -> (target :?> StackPanel).HorizontalSnapPointsChanged)

                    ComponentStackPanel._HorizontalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentStackPanel._HorizontalSnapPointsChanged

    static member VerticalSnapPointsChanged =
        if not ComponentStackPanel._VerticalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentStackPanel._VerticalSnapPointsChangedInit then
                    ComponentStackPanel._VerticalSnapPointsChanged <-
                        Attributes.Component.defineEvent "StackPanel_VerticalSnapPointsChanged" (fun target -> (target :?> StackPanel).VerticalSnapPointsChanged)

                    ComponentStackPanel._VerticalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentStackPanel._VerticalSnapPointsChanged

type ComponentStackPanelModifiers =

    /// <summary>Listens to the StackPanel HorizontalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the HorizontalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onHorizontalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabStackPanel>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentStackPanel.HorizontalSnapPointsChanged.WithValue(fn))

    /// <summary>Listens to the StackPanel VerticalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the VerticalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onVerticalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabStackPanel>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentStackPanel.VerticalSnapPointsChanged.WithValue(fn))
