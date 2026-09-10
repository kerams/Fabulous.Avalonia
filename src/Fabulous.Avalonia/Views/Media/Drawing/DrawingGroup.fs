namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections

type IFabDrawingGroup =
    inherit IFabDrawing

module DrawingGroup =
    let WidgetKey = Widgets.register<DrawingGroup>()

    let Children =
        Attributes.defineAvaloniaListWidgetCollection "DrawingGroup_Children" (fun target -> (target :?> DrawingGroup).Children)

    let Opacity =
        Attributes.defineAvaloniaPropertyWithEquality DrawingGroup.OpacityProperty

    let Transform =
        Attributes.defineAvaloniaPropertyWithEquality DrawingGroup.TransformProperty

    let TransformWidget =
        Attributes.defineAvaloniaPropertyWidget DrawingGroup.TransformProperty

    let ClipGeometry =
        Attributes.defineAvaloniaPropertyWidget DrawingGroup.ClipGeometryProperty

    let OpacityMaskWidget =
        Attributes.defineAvaloniaPropertyWidget DrawingGroup.OpacityMaskProperty

    let OpacityMask =
        Attributes.defineAvaloniaPropertyWithEquality DrawingGroup.OpacityMaskProperty

[<AutoOpen>]
module DrawingGroupBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DrawingGroup widget.</summary>
        static member DrawingGroup() =
            let attr = DrawingGroup.Children
            let scalar = DrawingGroup.Opacity.WithValue(1.0)
            CollectionBuilder<'msg, IFabDrawingGroup, IFabDrawing>(DrawingGroup.WidgetKey, attr, scalar)

        /// <summary>Creates a DrawingGroup widget.</summary>
        /// <param name="opacity">The opacity of the drawing group.</param>
        static member DrawingGroup(opacity: float) =
            let attr = DrawingGroup.Children
            let scalar = DrawingGroup.Opacity.WithValue(opacity)
            CollectionBuilder<'msg, IFabDrawingGroup, IFabDrawing>(DrawingGroup.WidgetKey, attr, scalar)

type DrawingGroupModifiers =

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: WidgetBuilder<'msg, #IFabBrush>) =
        let widget = DrawingGroup.OpacityMaskWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: IBrush) =
        this.AddScalar(DrawingGroup.OpacityMask.WithValue(value))

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: Color) =
        let brush = View.SolidColorBrush(value)
        DrawingGroupModifiers.opacityMask(this, brush)

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: string) =
        let brush = View.SolidColorBrush(value)
        DrawingGroupModifiers.opacityMask(this, brush)

    /// <summary>Sets the Transform property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Transform value.</param>
    [<Extension>]
    static member inline transform(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: WidgetBuilder<'msg, #IFabTransform>) =
        let widget = DrawingGroup.TransformWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the Transform property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Transform value.</param>
    [<Extension>]
    static member inline transform(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: string) =
        this.AddScalar(DrawingGroup.Transform.WithValue(Transform.Parse(value)))

    /// <summary>Sets the ClipGeometry property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ClipGeometry value.</param>
    [<Extension>]
    static member inline clipGeometry(this: WidgetBuilder<'msg, #IFabDrawingGroup>, value: WidgetBuilder<'msg, #IFabGeometry>) =
        let widget = DrawingGroup.ClipGeometry.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Link a ViewRef to access the direct DrawingGroup control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDrawingGroup>, value: ViewRef<DrawingGroup>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

type DrawingGroupCollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'itemType :> IFabDrawing>
        (_: CollectionBuilder<'msg, 'marker, IFabDrawing>, x: WidgetBuilder<'msg, 'itemType>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'itemType :> IFabDrawing>
        (_: CollectionBuilder<'msg, 'marker, IFabDrawing>, x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
