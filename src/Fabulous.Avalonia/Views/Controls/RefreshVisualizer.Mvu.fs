namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia


// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuRefreshVisualizer =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RefreshRequested: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.RefreshRequestedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RefreshRequestedInit: bool

    static member RefreshRequested =
        if not MvuRefreshVisualizer._RefreshRequestedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuRefreshVisualizer._RefreshRequestedInit then
                    MvuRefreshVisualizer._RefreshRequested <-
                        Attributes.Mvu.defineEvent<RefreshRequestedEventArgs> "RefreshVisualizer_RefreshRequested" (fun target ->
                            (target :?> RefreshVisualizer).RefreshRequested)

                    MvuRefreshVisualizer._RefreshRequestedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuRefreshVisualizer._RefreshRequested

type MvuRefreshVisualizerModifiers =
    /// <summary>Listens the RefreshVisualizer RefreshRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the RefreshRequested event is fired.</param>
    [<Extension>]
    static member inline onRefreshRequested(this: WidgetBuilder<'msg, #IFabRefreshVisualizer>, fn: RefreshRequestedEventArgs -> 'msg) =
        this.AddScalar(MvuRefreshVisualizer.RefreshRequested.WithValue(fn))
