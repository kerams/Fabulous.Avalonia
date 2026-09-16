namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTransform =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Changed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ChangedInit: bool

    static member Changed =
        if not MvuTransform._ChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTransform._ChangedInit then
                    MvuTransform._Changed <-
                        Attributes.Mvu.defineEventNoArg "Transform_Changed" (fun target -> (target :?> Transform).Changed)

                    MvuTransform._ChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTransform._Changed

type MvuTransformModifiers =
    /// <summary>Listens to the Transform changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Transform changes.</param>
    [<Extension>]
    static member inline onChanged(this: WidgetBuilder<'msg, #IFabTransform>, fn: 'msg) =
        this.AddScalar(MvuTransform.Changed.WithValue(MsgValue fn))
