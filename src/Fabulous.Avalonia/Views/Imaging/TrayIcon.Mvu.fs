namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Fabulous
open Avalonia.Controls
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTrayIcon =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Clicked: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClickedInit: bool

    static member Clicked =
        if not MvuTrayIcon._ClickedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTrayIcon._ClickedInit then
                    MvuTrayIcon._Clicked <-
                        Attributes.Mvu.defineEventNoArg "TrayIcon_Clicked" (fun target -> (target :?> TrayIcon).Clicked)

                    MvuTrayIcon._ClickedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTrayIcon._Clicked

type MvuTrayIconModifiers =
    /// <summary>Listens to the TrayIcon Clicked event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Clicked event fires.</param>
    [<Extension>]
    static member inline onClicked(this: WidgetBuilder<'msg, #IFabTrayIcon>, msg: 'msg) =
        this.AddScalar(MvuTrayIcon.Clicked.WithValue(MsgValue msg))
