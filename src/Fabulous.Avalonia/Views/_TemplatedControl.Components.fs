namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentTemplatedControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TemplateApplied: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.Primitives.TemplateAppliedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TemplateAppliedInit: bool

    static member TemplateApplied =
        if not ComponentTemplatedControl._TemplateAppliedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTemplatedControl._TemplateAppliedInit then
                    ComponentTemplatedControl._TemplateApplied <-
                        Attributes.Component.defineEvent "TemplatedControl_TemplateApplied" (fun target -> (target :?> TemplatedControl).TemplateApplied)

                    ComponentTemplatedControl._TemplateAppliedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTemplatedControl._TemplateApplied

type ComponentTemplatedControlModifiers =
    /// <summary>Listens to the TemplateApplied event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the template is applied.</param>
    [<Extension>]
    static member inline onTemplateApplied(this: WidgetBuilder<'msg, #IFabTemplatedControl>, fn: TemplateAppliedEventArgs -> unit) =
        this.AddScalar(ComponentTemplatedControl.TemplateApplied.WithValue(fn))
