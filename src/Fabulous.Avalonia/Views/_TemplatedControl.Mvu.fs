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
type MvuTemplatedControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TemplateApplied: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.Primitives.TemplateAppliedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TemplateAppliedInit: bool

    static member TemplateApplied =
        if not MvuTemplatedControl._TemplateAppliedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTemplatedControl._TemplateAppliedInit then
                    MvuTemplatedControl._TemplateApplied <-
                        Attributes.Mvu.defineEvent "TemplatedControl_TemplateApplied" (fun target -> (target :?> TemplatedControl).TemplateApplied)

                    MvuTemplatedControl._TemplateAppliedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTemplatedControl._TemplateApplied

type MvuTemplatedControlModifiers =
    /// <summary>Listens to the TemplateApplied event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the template is applied.</param>
    [<Extension>]
    static member inline onTemplateApplied(this: WidgetBuilder<'msg, #IFabTemplatedControl>, fn: TemplateAppliedEventArgs -> 'msg) =
        this.AddScalar(MvuTemplatedControl.TemplateApplied.WithValue(fn))
