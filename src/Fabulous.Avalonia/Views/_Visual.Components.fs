namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentVisual =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToVisualTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.VisualTreeAttachmentEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToVisualTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromVisualTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.VisualTreeAttachmentEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromVisualTreeInit: bool

    static member AttachedToVisualTree =
        if not ComponentVisual._AttachedToVisualTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentVisual._AttachedToVisualTreeInit then
                    ComponentVisual._AttachedToVisualTree <-
                        Attributes.Component.defineEvent "VisualAttachedToVisualTree" (fun target -> (target :?> Visual).AttachedToVisualTree)

                    ComponentVisual._AttachedToVisualTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentVisual._AttachedToVisualTree

    static member DetachedFromVisualTree =
        if not ComponentVisual._DetachedFromVisualTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentVisual._DetachedFromVisualTreeInit then
                    ComponentVisual._DetachedFromVisualTree <-
                        Attributes.Component.defineEvent "VisualAttachedToVisualTree" (fun target -> (target :?> Visual).DetachedFromVisualTree)

                    ComponentVisual._DetachedFromVisualTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentVisual._DetachedFromVisualTree

type ComponentVisualModifiers =
    /// <summary>Listens to the Visual AttachedToVisualTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control is attached to a rooted visual tree.</param>
    [<Extension>]
    static member inline onAttachedToVisualTree(this: WidgetBuilder<'msg, #IFabVisual>, fn: VisualTreeAttachmentEventArgs -> unit) =
        this.AddScalar(ComponentVisual.AttachedToVisualTree.WithValue(fn))

    /// <summary>Listens to the Visual DetachedFromVisualTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control is detached from a rooted visual tree.</param>
    [<Extension>]
    static member inline onDetachedFromVisualTree(this: WidgetBuilder<'msg, #IFabVisual>, fn: VisualTreeAttachmentEventArgs -> unit) =
        this.AddScalar(ComponentVisual.DetachedFromVisualTree.WithValue(fn))

type ComponentVisualExtraModifiers =
    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabVisual>, value: Color) =
        let brush = View.SolidColorBrush(value)
        VisualModifiers.opacityMask(this, brush)

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabVisual>, value: string) =
        let brush = View.SolidColorBrush(value)
        VisualModifiers.opacityMask(this, brush)
