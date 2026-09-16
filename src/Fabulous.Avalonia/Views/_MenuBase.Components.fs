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
type ComponentMenuBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosedInit: bool

    static member Opened =
        if not ComponentMenuBase._OpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuBase._OpenedInit then
                    ComponentMenuBase._Opened <-
                        Attributes.Component.defineEvent "MenuBase_Opened" (fun target -> (target :?> MenuBase).Opened)

                    ComponentMenuBase._OpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuBase._Opened

    static member Closed =
        if not ComponentMenuBase._ClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuBase._ClosedInit then
                    ComponentMenuBase._Closed <-
                        Attributes.Component.defineEvent "MenuBase_Closed" (fun target -> (target :?> MenuBase).Closed)

                    ComponentMenuBase._ClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuBase._Closed

type ComponentMenuBaseModifiers =
    /// <summary>Listens to the MenuOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Menu is opened.</param>
    [<Extension>]
    static member inline onOpened(this: WidgetBuilder<'msg, #IFabMenuBase>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentMenuBase.Opened.WithValue(fn))

    /// <summary>Listens to the MenuClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Menu is closed.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabMenuBase>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentMenuBase.Closed.WithValue(fn))
