namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Input
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentDragDrop =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragEnter: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragEnterInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragLeave: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragLeaveInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragOver: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DragOverInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Drop: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.DragEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropInit: bool

    static member DragEnter =
        if not ComponentDragDrop._DragEnterInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentDragDrop._DragEnterInit then
                    ComponentDragDrop._DragEnter <-
                        Attributes.Component.defineRoutedEvent<DragEventArgs> "DragDrop_DragEnter" DragDrop.DragEnterEvent

                    ComponentDragDrop._DragEnterInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentDragDrop._DragEnter

    static member DragLeave =
        if not ComponentDragDrop._DragLeaveInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentDragDrop._DragLeaveInit then
                    ComponentDragDrop._DragLeave <-
                        Attributes.Component.defineRoutedEvent<DragEventArgs> "DragDrop_DragLeave" DragDrop.DragLeaveEvent

                    ComponentDragDrop._DragLeaveInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentDragDrop._DragLeave

    static member DragOver =
        if not ComponentDragDrop._DragOverInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentDragDrop._DragOverInit then
                    ComponentDragDrop._DragOver <-
                        Attributes.Component.defineRoutedEvent<DragEventArgs> "DragDrop_DragOver" DragDrop.DragOverEvent

                    ComponentDragDrop._DragOverInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentDragDrop._DragOver

    static member Drop =
        if not ComponentDragDrop._DropInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentDragDrop._DropInit then
                    ComponentDragDrop._Drop <-
                        Attributes.Component.defineRoutedEvent<DragEventArgs> "DragDrop_Drop" DragDrop.DropEvent

                    ComponentDragDrop._DropInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentDragDrop._Drop

type ComponentDragDropModifiers =
    /// <summary>Listens to the DragDrop DragEnter event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation enters the element.</param>
    [<Extension>]
    static member inline onDragEnter(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> unit) =
        this.AddScalar(ComponentDragDrop.DragEnter.WithValue(fn))

    /// <summary>Listens to the DragDrop DragLeave event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation leaves the element.</param>
    [<Extension>]
    static member inline onDragLeave(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> unit) =
        this.AddScalar(ComponentDragDrop.DragLeave.WithValue(fn))

    /// <summary>Listens to the DragDrop DragOver event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation is in progress over the element.</param>
    [<Extension>]
    static member inline onDragOver(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> unit) =
        this.AddScalar(ComponentDragDrop.DragOver.WithValue(fn))

    /// <summary>Listens to the DragDrop Drop event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a drag-and-drop operation is dropped on the element.</param>
    [<Extension>]
    static member inline onDrop(this: WidgetBuilder<'msg, #IFabInteractive>, fn: DragEventArgs -> unit) =
        this.AddScalar(ComponentDragDrop.Drop.WithValue(fn))
