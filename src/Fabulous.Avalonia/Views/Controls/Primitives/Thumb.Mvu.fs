namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Avalonia.Input
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuThumb =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragStarted: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.VectorEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragStartedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragDelta: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.VectorEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragDeltaInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragCompleted: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.VectorEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragCompletedInit: bool

    static member DragStarted =
        if not MvuThumb._DragStartedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuThumb._DragStartedInit then
                    MvuThumb._DragStarted <-
                        Attributes.Mvu.defineEvent<VectorEventArgs> "Thumb_DragStarted" (fun target -> (target :?> Thumb).DragStarted)

                    MvuThumb._DragStartedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuThumb._DragStarted

    static member DragDelta =
        if not MvuThumb._DragDeltaInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuThumb._DragDeltaInit then
                    MvuThumb._DragDelta <-
                        Attributes.Mvu.defineEvent<VectorEventArgs> "Thumb_DragDelta" (fun target -> (target :?> Thumb).DragDelta)

                    MvuThumb._DragDeltaInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuThumb._DragDelta

    static member DragCompleted =
        if not MvuThumb._DragCompletedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuThumb._DragCompletedInit then
                    MvuThumb._DragCompleted <-
                        Attributes.Mvu.defineEvent<VectorEventArgs> "Thumb_DragCompleted" (fun target -> (target :?> Thumb).DragCompleted)

                    MvuThumb._DragCompletedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuThumb._DragCompleted


type MvuThumbModifiers =

    /// <summary>Listens to the Thumb DragStarted event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Thumb dragged started.</param>
    [<Extension>]
    static member inline onDragStarted(this: WidgetBuilder<'msg, #IFabThumb>, fn: VectorEventArgs -> 'msg) =
        this.AddScalar(MvuThumb.DragStarted.WithValue(fn))

    /// <summary>Listens to the Thumb DragDelta event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Thumb dragged.</param>
    [<Extension>]
    static member inline onDragDelta(this: WidgetBuilder<'msg, #IFabThumb>, fn: VectorEventArgs -> 'msg) =
        this.AddScalar(MvuThumb.DragDelta.WithValue(fn))

    /// <summary>Listens to the Thumb DragCompleted event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Thumb dragged is completed.</param>
    [<Extension>]
    static member inline onDragCompleted(this: WidgetBuilder<'msg, #IFabThumb>, fn: VectorEventArgs -> 'msg) =
        this.AddScalar(MvuThumb.DragCompleted.WithValue(fn))
