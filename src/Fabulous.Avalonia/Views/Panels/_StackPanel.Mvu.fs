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
type MvuStackPanel =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _HorizontalSnapPointsChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _VerticalSnapPointsChangedInit: bool

    static member HorizontalSnapPointsChanged =
        if not MvuStackPanel._HorizontalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuStackPanel._HorizontalSnapPointsChangedInit then
                    MvuStackPanel._HorizontalSnapPointsChanged <-
                        Attributes.Mvu.defineEvent "StackPanel_HorizontalSnapPointsChanged" (fun target -> (target :?> StackPanel).HorizontalSnapPointsChanged)

                    MvuStackPanel._HorizontalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuStackPanel._HorizontalSnapPointsChanged

    static member VerticalSnapPointsChanged =
        if not MvuStackPanel._VerticalSnapPointsChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuStackPanel._VerticalSnapPointsChangedInit then
                    MvuStackPanel._VerticalSnapPointsChanged <-
                        Attributes.Mvu.defineEvent "StackPanel_VerticalSnapPointsChanged" (fun target -> (target :?> StackPanel).VerticalSnapPointsChanged)

                    MvuStackPanel._VerticalSnapPointsChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuStackPanel._VerticalSnapPointsChanged

type MvuStackPanelModifiers =

    /// <summary>Listens to the StackPanel HorizontalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the HorizontalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onHorizontalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabStackPanel>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuStackPanel.HorizontalSnapPointsChanged.WithValue(fn))

    /// <summary>Listens to the StackPanel VerticalSnapPointsChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the VerticalSnapPointsChanged event fires.</param>
    [<Extension>]
    static member inline onVerticalSnapPointsChanged(this: WidgetBuilder<'msg, #IFabStackPanel>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuStackPanel.VerticalSnapPointsChanged.WithValue(fn))
