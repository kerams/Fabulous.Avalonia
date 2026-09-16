namespace Fabulous.Avalonia

open System.ComponentModel
open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentPopupFlyoutBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.ComponentModel.CancelEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosingInit: bool

    static member Opening =
        if not ComponentPopupFlyoutBase._OpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentPopupFlyoutBase._OpeningInit then
                    ComponentPopupFlyoutBase._Opening <-
                        Attributes.Component.defineEventNoArg "PopupFlyoutBase_Opening" (fun target -> (target :?> PopupFlyoutBase).Opening)

                    ComponentPopupFlyoutBase._OpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentPopupFlyoutBase._Opening

    static member Closing =
        if not ComponentPopupFlyoutBase._ClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentPopupFlyoutBase._ClosingInit then
                    ComponentPopupFlyoutBase._Closing <-
                        Attributes.Component.defineEvent "PopupFlyoutBase_Closing" (fun target -> (target :?> PopupFlyoutBase).Closing)

                    ComponentPopupFlyoutBase._ClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentPopupFlyoutBase._Closing

type ComponentPopupFlyoutBaseModifiers =
    /// <summary>Listens to the PopupFlyoutBase Opening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PopupFlyoutBase is opening.</param>
    [<Extension>]
    static member inline onOpening<'msg when 'msg: equality>(this: WidgetBuilder<'msg, IFabPopupFlyoutBase>, fn: unit -> unit) =
        this.AddScalar(ComponentPopupFlyoutBase.Opening.WithValue(fn))

    /// <summary>Listens to the PopupFlyoutBase Closing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PopupFlyoutBase is closing.</param>
    [<Extension>]
    static member inline onClosing<'msg when 'msg: equality>(this: WidgetBuilder<'msg, IFabPopupFlyoutBase>, fn: CancelEventArgs -> unit) =
        this.AddScalar(ComponentPopupFlyoutBase.Closing.WithValue(fn))
