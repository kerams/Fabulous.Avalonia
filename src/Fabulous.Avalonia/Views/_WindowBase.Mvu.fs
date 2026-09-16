namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuWindowBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Activated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Deactivated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DeactivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PositionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PixelPointEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PositionChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Resized: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.WindowResizedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResizedInit: bool

    static member Activated =
        if not MvuWindowBase._ActivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindowBase._ActivatedInit then
                    MvuWindowBase._Activated <-
                        Attributes.Mvu.defineEventNoArg "WindowBase_Activated" (fun target -> (target :?> WindowBase).Activated)

                    MvuWindowBase._ActivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindowBase._Activated

    static member Deactivated =
        if not MvuWindowBase._DeactivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindowBase._DeactivatedInit then
                    MvuWindowBase._Deactivated <-
                        Attributes.Mvu.defineEventNoArg "WindowBase_Deactivated" (fun target -> (target :?> WindowBase).Deactivated)

                    MvuWindowBase._DeactivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindowBase._Deactivated

    static member PositionChanged =
        if not MvuWindowBase._PositionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindowBase._PositionChangedInit then
                    MvuWindowBase._PositionChanged <-
                        Attributes.Mvu.defineEvent "WindowBase_PositionChanged" (fun target -> (target :?> WindowBase).PositionChanged)

                    MvuWindowBase._PositionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindowBase._PositionChanged

    static member Resized =
        if not MvuWindowBase._ResizedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuWindowBase._ResizedInit then
                    MvuWindowBase._Resized <-
                        Attributes.Mvu.defineEvent "WindowBase_Resized" (fun target -> (target :?> WindowBase).Resized)

                    MvuWindowBase._ResizedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuWindowBase._Resized

type MvuWindowBaseModifiers =
    /// <summary>Listens to the WindowBase Activated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is activated.</param>
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: 'msg) =
        this.AddScalar(MvuWindowBase.Activated.WithValue(MsgValue fn))

    /// <summary>Listens to the WindowBase Deactivated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is deactivated.</param>
    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: 'msg) =
        this.AddScalar(MvuWindowBase.Deactivated.WithValue(MsgValue fn))

    /// <summary>Listens to the WindowBase PositionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window position is changed.</param>
    [<Extension>]
    static member inline onPositionChanged(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: PixelPointEventArgs -> 'msg) =
        this.AddScalar(MvuWindowBase.PositionChanged.WithValue(fn))

    /// <summary>Listens to the WindowBase Resized event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is resized.</param>
    [<Extension>]
    static member inline onResized(this: WidgetBuilder<'msg, #IFabWindowBase>, fn: WindowResizedEventArgs -> 'msg) =
        this.AddScalar(MvuWindowBase.Resized.WithValue(fn))
