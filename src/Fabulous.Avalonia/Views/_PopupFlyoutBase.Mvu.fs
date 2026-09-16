namespace Fabulous.Avalonia

open System.ComponentModel
open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuPopupFlyoutBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.ComponentModel.CancelEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosingInit: bool

    static member Opening =
        if not MvuPopupFlyoutBase._OpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuPopupFlyoutBase._OpeningInit then
                    MvuPopupFlyoutBase._Opening <-
                        Attributes.Mvu.defineEventNoArg "PopupFlyoutBase_Opening" (fun target -> (target :?> PopupFlyoutBase).Opening)

                    MvuPopupFlyoutBase._OpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuPopupFlyoutBase._Opening

    static member Closing =
        if not MvuPopupFlyoutBase._ClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuPopupFlyoutBase._ClosingInit then
                    MvuPopupFlyoutBase._Closing <-
                        Attributes.Mvu.defineEvent "PopupFlyoutBase_Closing" (fun target -> (target :?> PopupFlyoutBase).Closing)

                    MvuPopupFlyoutBase._ClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuPopupFlyoutBase._Closing

type MvuPopupFlyoutBaseModifiers =
    /// <summary>Listens to the PopupFlyoutBase Opening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PopupFlyoutBase is opening.</param>
    [<Extension>]
    static member inline onOpening(this: WidgetBuilder<'msg, #IFabPopupFlyoutBase>, fn: 'msg) =
        this.AddScalar(MvuPopupFlyoutBase.Opening.WithValue(MsgValue fn))

    /// <summary>Listens to the PopupFlyoutBase Closing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PopupFlyoutBase is closing.</param>
    [<Extension>]
    static member inline onClosing(this: WidgetBuilder<'msg, #IFabPopupFlyoutBase>, fn: CancelEventArgs -> 'msg) =
        this.AddScalar(MvuPopupFlyoutBase.Closing.WithValue(fn))
