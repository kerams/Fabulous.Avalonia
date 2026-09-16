namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuMenuBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosedInit: bool

    static member Opened =
        if not MvuMenuBase._OpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuBase._OpenedInit then
                    MvuMenuBase._Opened <-
                        Attributes.Mvu.defineEvent "MenuBase_Opened" (fun target -> (target :?> MenuBase).Opened)

                    MvuMenuBase._OpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuBase._Opened

    static member Closed =
        if not MvuMenuBase._ClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuBase._ClosedInit then
                    MvuMenuBase._Closed <-
                        Attributes.Mvu.defineEvent "MenuBase_Closed" (fun target -> (target :?> MenuBase).Closed)

                    MvuMenuBase._ClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuBase._Closed

type MvuMenuBaseModifiers =
    /// <summary>Listens to the MenuOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Menu is opened.</param>
    [<Extension>]
    static member inline onOpened(this: WidgetBuilder<'msg, #IFabMenuBase>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuMenuBase.Opened.WithValue(fn))

    /// <summary>Listens to the MenuClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Menu is closed.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabMenuBase>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuMenuBase.Closed.WithValue(fn))
