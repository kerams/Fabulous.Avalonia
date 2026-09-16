namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentThemeVariantScope =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    static member ActualThemeVariantChanged =
        if not ComponentThemeVariantScope._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentThemeVariantScope._ActualThemeVariantChangedInit then
                    ComponentThemeVariantScope._ActualThemeVariantChanged <-
                        Attributes.Component.defineEventNoArg "TopLevel_ThemeVariantChanged" (fun target -> (target :?> ThemeVariantScope).ActualThemeVariantChanged)

                    ComponentThemeVariantScope._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentThemeVariantScope._ActualThemeVariantChanged

type ComponentThemeVariantScopeModifiers =

    /// <summary>Listens the ThemeVariantScope ThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the ThemeVariantChanged event is raised.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabThemeVariantScope>, fn: unit -> unit) =
        this.AddScalar(ComponentThemeVariantScope.ActualThemeVariantChanged.WithValue(fn))
