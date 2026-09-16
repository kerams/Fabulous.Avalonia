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
type MvuWindow =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.WindowClosingEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowOpenedInit: bool

    static member WindowClosing =
        if not MvuWindow._WindowClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindow._WindowClosingInit then
                    MvuWindow._WindowClosing <-
                        Attributes.Mvu.defineEvent "Window_Closing" (fun target -> (target :?> Window).Closing)

                    MvuWindow._WindowClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindow._WindowClosing

    static member WindowClosed =
        if not MvuWindow._WindowClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindow._WindowClosedInit then
                    MvuWindow._WindowClosed <-
                        Attributes.Mvu.defineRoutedEvent "Window_Closed" Window.WindowClosedEvent

                    MvuWindow._WindowClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindow._WindowClosed

    static member WindowOpened =
        if not MvuWindow._WindowOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindow._WindowOpenedInit then
                    MvuWindow._WindowOpened <-
                        Attributes.Mvu.defineRoutedEvent "Window_Opened" Window.WindowOpenedEvent

                    MvuWindow._WindowOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindow._WindowOpened

type MvuWindowModifiers =
    /// <summary>Listens to the Window WindowClosing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closing.</param>
    [<Extension>]
    static member inline onWindowClosing(this: WidgetBuilder<'msg, #IFabWindow>, fn: WindowClosingEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowClosing.WithValue(fn))

    /// <summary>Listens to the Window WindowClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closed.</param>
    [<Extension>]
    static member inline onWindowClosed(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowClosed.WithValue(fn))

    /// <summary>Listens to the Window WindowOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is opened.</param>
    [<Extension>]
    static member inline onWindowOpened(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowOpened.WithValue(fn))
