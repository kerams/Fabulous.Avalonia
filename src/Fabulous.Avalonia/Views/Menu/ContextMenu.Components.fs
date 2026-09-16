namespace Fabulous.Avalonia.Components

open System.ComponentModel
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentContextMenu =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.ComponentModel.CancelEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(System.ComponentModel.CancelEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosingInit: bool

    static member Opening =
        if not ComponentContextMenu._OpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentContextMenu._OpeningInit then
                    ComponentContextMenu._Opening <-
                        Attributes.Component.defineEventHandler "ContextMenu_Opening" (fun target -> (target :?> ContextMenu).Opening)

                    ComponentContextMenu._OpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentContextMenu._Opening

    static member Closing =
        if not ComponentContextMenu._ClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentContextMenu._ClosingInit then
                    ComponentContextMenu._Closing <-
                        Attributes.Component.defineEventHandler "ContextMenu_Closing" (fun target -> (target :?> ContextMenu).Closing)

                    ComponentContextMenu._ClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentContextMenu._Closing

type ComponentContextMenuModifiers =
    /// <summary>Listens to the ContextMenu Opening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Opening event fires.</param>
    [<Extension>]
    static member inline onOpening(this: WidgetBuilder<'msg, #IFabContextMenu>, fn: CancelEventArgs -> unit) =
        this.AddScalar(ComponentContextMenu.Opening.WithValue(fn))

    /// <summary>Listens to the ContextMenu Closing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Closing event fires.</param>
    [<Extension>]
    static member inline onClosing(this: WidgetBuilder<'msg, #IFabContextMenu>, fn: CancelEventArgs -> unit) =
        this.AddScalar(ComponentContextMenu.Closing.WithValue(fn))
