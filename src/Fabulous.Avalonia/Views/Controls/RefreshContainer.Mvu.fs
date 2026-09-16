namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuRefreshContainer =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RefreshRequested: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.RefreshRequestedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RefreshRequestedInit: bool

    static member RefreshRequested =
        if not MvuRefreshContainer._RefreshRequestedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuRefreshContainer._RefreshRequestedInit then
                    MvuRefreshContainer._RefreshRequested <-
                        Attributes.Mvu.defineEvent<RefreshRequestedEventArgs> "RefreshContainer_RefreshRequested" (fun target -> (target :?> RefreshContainer).RefreshRequested)

                    MvuRefreshContainer._RefreshRequestedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuRefreshContainer._RefreshRequested

type MvuRefreshContainerModifiers =
    /// <summary>Listens the RefreshContainer RefreshRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the RefreshRequested event is fired.</param>
    [<Extension>]
    static member inline onRefreshRequested(this: WidgetBuilder<'msg, #IFabRefreshContainer>, fn: RefreshRequestedEventArgs -> 'msg) =
        this.AddScalar(MvuRefreshContainer.RefreshRequested.WithValue(fn))
