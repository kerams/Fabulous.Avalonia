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
type ComponentWindow =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.WindowClosingEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowOpenedInit: bool

    static member WindowClosing =
        if not ComponentWindow._WindowClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindow._WindowClosingInit then
                    ComponentWindow._WindowClosing <-
                        Attributes.Component.defineEvent "Window_Closing" (fun target -> (target :?> Window).Closing)

                    ComponentWindow._WindowClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindow._WindowClosing

    static member WindowClosed =
        if not ComponentWindow._WindowClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindow._WindowClosedInit then
                    ComponentWindow._WindowClosed <-
                        Attributes.Component.defineRoutedEvent "Window_Closed" Window.WindowClosedEvent

                    ComponentWindow._WindowClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindow._WindowClosed

    static member WindowOpened =
        if not ComponentWindow._WindowOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindow._WindowOpenedInit then
                    ComponentWindow._WindowOpened <-
                        Attributes.Component.defineRoutedEvent "Window_Opened" Window.WindowOpenedEvent

                    ComponentWindow._WindowOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindow._WindowOpened

type ComponentWindowModifiers =
    /// <summary>Listens to the Window WindowClosing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closing.</param>
    [<Extension>]
    static member inline onWindowClosing(this: WidgetBuilder<'msg, #IFabWindow>, fn: WindowClosingEventArgs -> unit) =
        this.AddScalar(ComponentWindow.WindowClosing.WithValue(fn))

    /// <summary>Listens to the Window WindowClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closed.</param>
    [<Extension>]
    static member inline onWindowClosed(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentWindow.WindowClosed.WithValue(fn))

    /// <summary>Listens to the Window WindowOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is opened.</param>
    [<Extension>]
    static member inline onWindowOpened(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentWindow.WindowOpened.WithValue(fn))
