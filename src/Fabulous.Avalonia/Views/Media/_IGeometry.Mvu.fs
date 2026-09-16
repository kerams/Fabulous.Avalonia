namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuGeometry =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Changed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ChangedInit: bool

    static member Changed =
        if not MvuGeometry._ChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuGeometry._ChangedInit then
                    MvuGeometry._Changed <-
                        Attributes.Mvu.defineEventNoArg "Geometry_Changed" (fun target -> (target :?> Geometry).Changed)

                    MvuGeometry._ChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuGeometry._Changed

type MvuGeometryModifiers =
    /// <summary>Listens to the Geometry Changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the geometry changes.</param>
    [<Extension>]
    static member inline onChanged(this: WidgetBuilder<'msg, #IFabGeometry>, msg: 'msg) =
        this.AddScalar(MvuGeometry.Changed.WithValue(MsgValue msg))
