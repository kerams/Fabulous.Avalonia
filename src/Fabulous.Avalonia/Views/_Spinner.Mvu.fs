namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuSpinner =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Spin: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SpinEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SpinInit: bool

    static member Spin =
        if not MvuSpinner._SpinInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSpinner._SpinInit then
                    MvuSpinner._Spin <-
                        Attributes.Mvu.defineEvent<SpinEventArgs> "Spinner_Spin" (fun target -> (target :?> Spinner).Spin)

                    MvuSpinner._SpinInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSpinner._Spin
