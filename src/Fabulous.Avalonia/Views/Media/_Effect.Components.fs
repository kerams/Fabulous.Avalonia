namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentEffect =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Invalidated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _InvalidatedInit: bool

    static member Invalidated =
        if not ComponentEffect._InvalidatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentEffect._InvalidatedInit then
                    ComponentEffect._Invalidated <-
                        Attributes.Component.defineEventNoArg "Effect_Invalidated" (fun target -> (target :?> Effect).Invalidated)

                    ComponentEffect._InvalidatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentEffect._Invalidated

type ComponentEffectModifiers =
    /// <summary>Listens the Effect Invalidated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Effect is invalidated.</param>
    [<Extension>]
    static member inline onInvalidated(this: WidgetBuilder<'msg, #IFabEffect>, msg: unit -> unit) =
        this.AddScalar(ComponentEffect.Invalidated.WithValue(msg))
