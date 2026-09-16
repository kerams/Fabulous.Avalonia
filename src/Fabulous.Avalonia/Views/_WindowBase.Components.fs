namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentWindowBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Activated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Deactivated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DeactivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PositionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PixelPointEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PositionChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Resized: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.WindowResizedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResizedInit: bool

    static member Activated =
        if not ComponentWindowBase._ActivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindowBase._ActivatedInit then
                    ComponentWindowBase._Activated <-
                        Attributes.Component.defineEventNoArg "WindowBase_Activated" (fun target -> (target :?> WindowBase).Activated)

                    ComponentWindowBase._ActivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindowBase._Activated

    static member Deactivated =
        if not ComponentWindowBase._DeactivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindowBase._DeactivatedInit then
                    ComponentWindowBase._Deactivated <-
                        Attributes.Component.defineEventNoArg "WindowBase_Deactivated" (fun target -> (target :?> WindowBase).Deactivated)

                    ComponentWindowBase._DeactivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindowBase._Deactivated

    static member PositionChanged =
        if not ComponentWindowBase._PositionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindowBase._PositionChangedInit then
                    ComponentWindowBase._PositionChanged <-
                        Attributes.Component.defineEvent "WindowBase_PositionChanged" (fun target -> (target :?> WindowBase).PositionChanged)

                    ComponentWindowBase._PositionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindowBase._PositionChanged

    static member Resized =
        if not ComponentWindowBase._ResizedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentWindowBase._ResizedInit then
                    ComponentWindowBase._Resized <-
                        Attributes.Component.defineEvent "WindowBase_Resized" (fun target -> (target :?> WindowBase).Resized)

                    ComponentWindowBase._ResizedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentWindowBase._Resized

type ComponentWindowBaseModifiers =
    /// <summary>Listens to the WindowBase Activated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the window is activated.</param>
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabWindowBase>, msg: unit -> unit) =
        this.AddScalar(ComponentWindowBase.Activated.WithValue(msg))

    /// <summary>Listens to the WindowBase Deactivated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the window is deactivated.</param>
    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabWindowBase>, msg: unit -> unit) =
        this.AddScalar(ComponentWindowBase.Deactivated.WithValue(msg))

    /// <summary>Listens to the WindowBase PositionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window position is changed.</param>
    [<Extension>]
    static member inline onPositionChanged(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: PixelPointEventArgs -> unit) =
        this.AddScalar(ComponentWindowBase.PositionChanged.WithValue(fn))

    /// <summary>Listens to the WindowBase Resized event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is resized.</param>
    [<Extension>]
    static member inline onResized(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: WindowResizedEventArgs -> unit) =
        this.AddScalar(ComponentWindowBase.Resized.WithValue(fn))
