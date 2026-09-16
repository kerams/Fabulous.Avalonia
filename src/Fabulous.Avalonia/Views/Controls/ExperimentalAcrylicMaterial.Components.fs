namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentExperimentalAcrylicMaterial =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Invalidated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _InvalidatedInit: bool

    static member Invalidated =
        if not ComponentExperimentalAcrylicMaterial._InvalidatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentExperimentalAcrylicMaterial._InvalidatedInit then
                    ComponentExperimentalAcrylicMaterial._Invalidated <-
                        Attributes.Component.defineEventNoArg "ExperimentalAcrylicMaterial_Invalidated" (fun target -> (target :?> ExperimentalAcrylicMaterial).Invalidated)

                    ComponentExperimentalAcrylicMaterial._InvalidatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentExperimentalAcrylicMaterial._Invalidated

type ComponentExperimentalAcrylicMaterialModifiers =
    /// <summary>Listens the ExperimentalAcrylicMaterial Invalidated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the ExperimentalAcrylicMaterial is invalidated.</param>
    [<Extension>]
    static member inline onInvalidated(this: WidgetBuilder<'msg, #IFabExperimentalAcrylicMaterial>, fn: unit -> unit) =
        this.AddScalar(ComponentExperimentalAcrylicMaterial.Invalidated.WithValue(fn))
