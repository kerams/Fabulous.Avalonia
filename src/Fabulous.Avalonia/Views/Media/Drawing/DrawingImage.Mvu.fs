namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuDrawingImage =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Invalidated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _InvalidatedInit: bool

    static member Invalidated =
        if not MvuDrawingImage._InvalidatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDrawingImage._InvalidatedInit then
                    MvuDrawingImage._Invalidated <-
                        Attributes.Mvu.defineEventNoArg "DrawingImage_Invalidated" (fun target -> (target :?> DrawingImage).Invalidated)

                    MvuDrawingImage._InvalidatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDrawingImage._Invalidated


type MvuDrawingImageModifiers =
    /// <summary>Listens the DrawingImage Invalidated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the DrawingImage is invalidated.</param>
    [<Extension>]
    static member inline onInvalidated(this: WidgetBuilder<'msg, #IFabDrawingImage>, msg: 'msg) =
        this.AddScalar(MvuDrawingImage.Invalidated.WithValue(MsgValue msg))
