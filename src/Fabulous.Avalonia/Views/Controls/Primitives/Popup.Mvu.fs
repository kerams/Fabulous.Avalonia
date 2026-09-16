namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuPopup =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.EventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpenedInit: bool

    static member Closed =
        if not MvuPopup._ClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuPopup._ClosedInit then
                    MvuPopup._Closed <-
                        Attributes.Mvu.defineEvent "Popup_Closed" (fun target -> (target :?> Popup).Closed)

                    MvuPopup._ClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuPopup._Closed

    static member Opened =
        if not MvuPopup._OpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuPopup._OpenedInit then
                    MvuPopup._Opened <-
                        Attributes.Mvu.defineEventNoArg "Popup_Opened" (fun target -> (target :?> Popup).Opened)

                    MvuPopup._OpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuPopup._Opened

type MvuPopupModifiers =
    /// <summary>Listens to the Popup Closed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Popup is closed.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabPopup>, msg: 'msg) =
        this.AddScalar(MvuPopup.Closed.WithValue(fun _ -> msg))

    /// <summary>Listens to the Popup Opened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Popup is opened.</param>
    [<Extension>]
    static member inline onOpened(this: WidgetBuilder<'msg, #IFabPopup>, msg: 'msg) =
        this.AddScalar(MvuPopup.Opened.WithValue(MsgValue msg))
