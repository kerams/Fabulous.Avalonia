namespace Fabulous.Avalonia

open Avalonia.Media
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentGradientBrush =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _GradientStops: Fabulous.WidgetCollectionAttributeDefinitions.WidgetCollectionAttributeDefinition

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _GradientStopsInit: bool

    static member GradientStops =
        if not ComponentGradientBrush._GradientStopsInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentGradientBrush._GradientStopsInit then
                    ComponentGradientBrush._GradientStops <-
                        Attributes.defineAvaloniaListWidgetCollection "GradientBrush_GradientStops" (fun target -> (target :?> GradientBrush).GradientStops)

                    ComponentGradientBrush._GradientStopsInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentGradientBrush._GradientStops
