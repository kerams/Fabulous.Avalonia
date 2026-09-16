namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentGeometry =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Changed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ChangedInit: bool

    static member Changed =
        if not ComponentGeometry._ChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentGeometry._ChangedInit then
                    ComponentGeometry._Changed <-
                        Attributes.Component.defineEventNoArg "Geometry_Changed" (fun target -> (target :?> Geometry).Changed)

                    ComponentGeometry._ChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentGeometry._Changed

type ComponentGeometryModifiers =
    /// <summary>Listens to the Geometry Changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the geometry changes.</param>
    [<Extension>]
    static member inline onChanged(this: WidgetBuilder<'msg, #IFabGeometry>, msg: unit -> unit) =
        this.AddScalar(ComponentGeometry.Changed.WithValue(msg))
