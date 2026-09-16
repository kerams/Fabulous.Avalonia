namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentSpinner =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Spin: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SpinEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SpinInit: bool

    static member Spin =
        if not ComponentSpinner._SpinInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSpinner._SpinInit then
                    ComponentSpinner._Spin <-
                        Attributes.Component.defineEvent<SpinEventArgs> "Spinner_Spin" (fun target -> (target :?> Spinner).Spin)

                    ComponentSpinner._SpinInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSpinner._Spin
