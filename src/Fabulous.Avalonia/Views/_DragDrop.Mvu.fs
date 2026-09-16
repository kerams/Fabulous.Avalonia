namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Input
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuDragDrop =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragEnter: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragEnterInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragLeave: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragLeaveInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragOver: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragOverInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Drop: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropInit: bool

    static member DragEnter =
        if not MvuDragDrop._DragEnterInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDragDrop._DragEnterInit then
                    MvuDragDrop._DragEnter <-
                        Attributes.Mvu.defineRoutedEvent<DragEventArgs> "DragDrop_DragEnter" DragDrop.DragEnterEvent

                    MvuDragDrop._DragEnterInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDragDrop._DragEnter

    static member DragLeave =
        if not MvuDragDrop._DragLeaveInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDragDrop._DragLeaveInit then
                    MvuDragDrop._DragLeave <-
                        Attributes.Mvu.defineRoutedEvent<DragEventArgs> "DragDrop_DragLeave" DragDrop.DragLeaveEvent

                    MvuDragDrop._DragLeaveInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDragDrop._DragLeave

    static member DragOver =
        if not MvuDragDrop._DragOverInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDragDrop._DragOverInit then
                    MvuDragDrop._DragOver <-
                        Attributes.Mvu.defineRoutedEvent<DragEventArgs> "DragDrop_DragOver" DragDrop.DragOverEvent

                    MvuDragDrop._DragOverInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDragDrop._DragOver

    static member Drop =
        if not MvuDragDrop._DropInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDragDrop._DropInit then
                    MvuDragDrop._Drop <-
                        Attributes.Mvu.defineRoutedEvent<DragEventArgs> "DragDrop_Drop" DragDrop.DropEvent

                    MvuDragDrop._DropInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDragDrop._Drop

type MvuDragDropModifiers =
    /// <summary>Listens to the DragDrop DragEnter event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation enters the element.</param>
    [<Extension>]
    static member inline onDragEnter(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> 'msg) =
        this.AddScalar(MvuDragDrop.DragEnter.WithValue(fn))

    /// <summary>Listens to the DragDrop DragLeave event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation leaves the element.</param>
    [<Extension>]
    static member inline onDragLeave(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> 'msg) =
        this.AddScalar(MvuDragDrop.DragLeave.WithValue(fn))

    /// <summary>Listens to the DragDrop DragOver event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation is in progress over the element.</param>
    [<Extension>]
    static member inline onDragOver(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> 'msg) =
        this.AddScalar(MvuDragDrop.DragOver.WithValue(fn))

    /// <summary>Listens to the DragDrop Drop event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation is dropped on the element.</param>
    [<Extension>]
    static member inline onDrop(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> 'msg) =
        this.AddScalar(MvuDragDrop.Drop.WithValue(fn))
