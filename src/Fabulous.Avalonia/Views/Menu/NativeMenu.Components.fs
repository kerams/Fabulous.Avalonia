namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentNativeMenu =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.EventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.EventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _NeedsUpdate: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.EventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _NeedsUpdateInit: bool

    static member Opening =
        if not ComponentNativeMenu._OpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNativeMenu._OpeningInit then
                    ComponentNativeMenu._Opening <-
                        Attributes.Component.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Opening)

                    ComponentNativeMenu._OpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNativeMenu._Opening

    static member Closed =
        if not ComponentNativeMenu._ClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNativeMenu._ClosedInit then
                    ComponentNativeMenu._Closed <-
                        Attributes.Component.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Closed)

                    ComponentNativeMenu._ClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNativeMenu._Closed

    static member NeedsUpdate =
        if not ComponentNativeMenu._NeedsUpdateInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNativeMenu._NeedsUpdateInit then
                    ComponentNativeMenu._NeedsUpdate <-
                        Attributes.Component.defineEvent "NativeMenu_NeedsUpdate" (fun target -> (target :?> NativeMenu).NeedsUpdate)

                    ComponentNativeMenu._NeedsUpdateInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNativeMenu._NeedsUpdate

type ComponentNativeMenuModifiers =
    /// <summary>Listens to the NativeMenu Opening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Opening event fires.</param>
    [<Extension>]
    static member inline onOpening(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> unit) =
        this.AddScalar(ComponentNativeMenu.Opening.WithValue(msg))

    /// <summary>Listens to the NativeMenu Closed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Closed event fires.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> unit) =
        this.AddScalar(ComponentNativeMenu.Closed.WithValue(msg))

    /// <summary>Listens to the NativeMenu NeedsUpdate event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the NeedsUpdate event fires.</param>
    [<Extension>]
    static member inline onNeedsUpdate(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> unit) =
        this.AddScalar(ComponentNativeMenu.NeedsUpdate.WithValue(msg))
