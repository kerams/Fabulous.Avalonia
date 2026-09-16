namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Layout
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentLayoutable =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _EffectiveViewportChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Layout.EffectiveViewportChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _EffectiveViewportChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _LayoutUpdated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _LayoutUpdatedInit: bool

    static member EffectiveViewportChanged =
        if not ComponentLayoutable._EffectiveViewportChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentLayoutable._EffectiveViewportChangedInit then
                    ComponentLayoutable._EffectiveViewportChanged <-
                        Attributes.Component.defineEvent<EffectiveViewportChangedEventArgs> "Layoutable_EffectiveViewportChanged" (fun target ->
                            (target :?> Layoutable).EffectiveViewportChanged)

                    ComponentLayoutable._EffectiveViewportChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentLayoutable._EffectiveViewportChanged

    static member LayoutUpdated =
        if not ComponentLayoutable._LayoutUpdatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentLayoutable._LayoutUpdatedInit then
                    ComponentLayoutable._LayoutUpdated <-
                        Attributes.Component.defineEventNoArg "Layoutable_LayoutUpdated" (fun target -> (target :?> Layoutable).LayoutUpdated)

                    ComponentLayoutable._LayoutUpdatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentLayoutable._LayoutUpdated

type ComponentLayoutableModifiers =
    /// <summary>Listens to the Layoutable EffectiveViewportChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the element's effective viewport changes.</param>
    [<Extension>]
    static member inline onEffectiveViewportChanged(this: WidgetBuilder<'msg, #IFabLayoutable>, fn: EffectiveViewportChangedEventArgs -> unit) =
        this.AddScalar(ComponentLayoutable.EffectiveViewportChanged.WithValue(fn))

    /// <summary>Listens to the Layoutable LayoutUpdated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the element's layout is updated.</param>
    [<Extension>]
    static member inline onLayoutUpdated(this: WidgetBuilder<'msg, #IFabLayoutable>, msg: unit -> unit) =
        this.AddScalar(ComponentLayoutable.LayoutUpdated.WithValue(msg))
