namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuEffect =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Invalidated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _InvalidatedInit: bool

    static member Invalidated =
        if not MvuEffect._InvalidatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuEffect._InvalidatedInit then
                    MvuEffect._Invalidated <-
                        Attributes.Mvu.defineEventNoArg "Effect_Invalidated" (fun target -> (target :?> Effect).Invalidated)

                    MvuEffect._InvalidatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuEffect._Invalidated

type MvuEffectModifiers =
    /// <summary>Listens the Effect Invalidated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Effect is invalidated.</param>
    [<Extension>]
    static member inline onInvalidated(this: WidgetBuilder<'msg, #IFabEffect>, msg: 'smg) =
        this.AddScalar(MvuEffect.Invalidated.WithValue(MsgValue msg))
