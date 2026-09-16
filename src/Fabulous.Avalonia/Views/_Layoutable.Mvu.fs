namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Layout
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuLayoutable =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _EffectiveViewportChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Layout.EffectiveViewportChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _EffectiveViewportChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _LayoutUpdated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _LayoutUpdatedInit: bool

    static member EffectiveViewportChanged =
        if not MvuLayoutable._EffectiveViewportChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuLayoutable._EffectiveViewportChangedInit then
                    MvuLayoutable._EffectiveViewportChanged <-
                        Attributes.Mvu.defineEvent<EffectiveViewportChangedEventArgs> "Layoutable_EffectiveViewportChanged" (fun target ->
                            (target :?> Layoutable).EffectiveViewportChanged)

                    MvuLayoutable._EffectiveViewportChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuLayoutable._EffectiveViewportChanged

    static member LayoutUpdated =
        if not MvuLayoutable._LayoutUpdatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuLayoutable._LayoutUpdatedInit then
                    MvuLayoutable._LayoutUpdated <-
                        Attributes.Mvu.defineEventNoArg "Layoutable_LayoutUpdated" (fun target -> (target :?> Layoutable).LayoutUpdated)

                    MvuLayoutable._LayoutUpdatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuLayoutable._LayoutUpdated

type MvuLayoutableModifiers =
    /// <summary>Listens to the Layoutable EffectiveViewportChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the element's effective viewport changes.</param>
    [<Extension>]
    static member inline onEffectiveViewportChanged(this: WidgetBuilder<'msg, #IFabLayoutable>, fn: EffectiveViewportChangedEventArgs -> 'msg) =
        this.AddScalar(MvuLayoutable.EffectiveViewportChanged.WithValue(fn))

    /// <summary>Listens to the Layoutable LayoutUpdated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the element's layout is updated.</param>
    [<Extension>]
    static member inline onLayoutUpdated(this: WidgetBuilder<'msg, #IFabLayoutable>, fn: 'msg) =
        this.AddScalar(MvuLayoutable.LayoutUpdated.WithValue(MsgValue fn))
