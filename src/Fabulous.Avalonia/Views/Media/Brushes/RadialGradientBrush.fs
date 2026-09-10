namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabRadialGradientBrush =
    inherit IFabGradientBrush

module RadialGradientBrush =
    let WidgetKey = Widgets.register<RadialGradientBrush>()

    let Center =
        Attributes.defineAvaloniaPropertyWithEquality RadialGradientBrush.CenterProperty

    let GradientOrigin =
        Attributes.defineAvaloniaPropertyWithEquality RadialGradientBrush.GradientOriginProperty

    let RadiusX =
        Attributes.defineAvaloniaPropertyWithEquality RadialGradientBrush.RadiusXProperty

    let RadiusY =
        Attributes.defineAvaloniaPropertyWithEquality RadialGradientBrush.RadiusYProperty

[<AutoOpen>]
module RadialGradientBrushBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a RadialGradientBrush widget.</summary>
        /// <param name="center">The center of the gradient.</param>
        static member RadialGradientBrush(center: RelativePoint) =
            let s1 = RadialGradientBrush.Center.WithValue(center)
            let s2 = RadialGradientBrush.GradientOrigin.WithValue(RelativePoint.Center)
            let scalars = StackList.two(s1, s2)
            let attr = ComponentGradientBrush.GradientStops
            CollectionBuilder<'msg, IFabRadialGradientBrush, IFabGradientStop>(
                RadialGradientBrush.WidgetKey,
                scalars,
                attr
            )

        /// <summary>Creates a RadialGradientBrush widget.</summary>
        /// <param name="center">The center of the gradient.</param>
        /// <param name="unit">The relative unit of the center.</param>
        static member RadialGradientBrush(center: Point, unit: RelativeUnit) =
            let s1 = RadialGradientBrush.Center.WithValue(RelativePoint(center, unit))
            let s2 = RadialGradientBrush.GradientOrigin.WithValue(RelativePoint.Center)
            let scalars = StackList.two(s1, s2)
            let attr = ComponentGradientBrush.GradientStops
            CollectionBuilder<'msg, IFabRadialGradientBrush, IFabGradientStop>(RadialGradientBrush.WidgetKey, scalars, attr)

        /// <summary>Creates a RadialGradientBrush widget.</summary>
        /// <param name="center">The center of the gradient.</param>
        /// <param name="origin">The origin of the gradient.</param>
        static member RadialGradientBrush(center: RelativePoint, origin: RelativePoint) =
            let s1 = RadialGradientBrush.Center.WithValue(center)
            let s2 = RadialGradientBrush.GradientOrigin.WithValue(origin)
            let scalars = StackList.two(s1, s2)
            let attr = ComponentGradientBrush.GradientStops
            CollectionBuilder<'msg, IFabRadialGradientBrush, IFabGradientStop>(
                RadialGradientBrush.WidgetKey,
                scalars,
                attr
            )

        /// <summary>Creates a RadialGradientBrush widget.</summary>
        /// <param name="center">The center of the gradient.</param>
        /// <param name="origin">The origin of the gradient.</param>
        /// <param name="unit">The relative unit of the center and origin.</param>
        static member RadialGradientBrush(center: Point, origin: Point, unit: RelativeUnit) =
            let s1 = RadialGradientBrush.Center.WithValue(RelativePoint(center, unit))
            let s2 = RadialGradientBrush.GradientOrigin.WithValue(RelativePoint(origin, unit))
            let scalars = StackList.two(s1, s2)
            let attr = ComponentGradientBrush.GradientStops
            CollectionBuilder<'msg, IFabRadialGradientBrush, IFabGradientStop>(RadialGradientBrush.WidgetKey, scalars, attr)

        /// <summary>Creates a RadialGradientBrush widget.</summary>
        static member RadialGradientBrush() =
            let s1 = RadialGradientBrush.Center.WithValue(RelativePoint.Center)
            let s2 = RadialGradientBrush.GradientOrigin.WithValue(RelativePoint.Center)
            let scalars = StackList.two(s1, s2)
            let attr = ComponentGradientBrush.GradientStops
            CollectionBuilder<'msg, IFabRadialGradientBrush, IFabGradientStop>(
                RadialGradientBrush.WidgetKey,
                scalars,
                attr
            )

type RadialGradientBrushModifiers =

    /// <summary>Sets the RadiusX property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Radius value.</param>
    [<Extension>]
    static member inline radiusX(this: WidgetBuilder<'msg, #IFabRadialGradientBrush>, value: float) =
        this.AddScalar(RadialGradientBrush.RadiusX.WithValue(RelativeScalar(value, RelativeUnit.Relative)))

    /// <summary>Sets the RadiusX property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Radius value.</param>
    [<Extension>]
    static member inline radiusX(this: WidgetBuilder<'msg, #IFabRadialGradientBrush>, value: RelativeScalar) =
        this.AddScalar(RadialGradientBrush.RadiusX.WithValue(value))

    /// <summary>Sets the RadiusY property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Radius value.</param>
    [<Extension>]
    static member inline radiusY(this: WidgetBuilder<'msg, #IFabRadialGradientBrush>, value: float) =
        this.AddScalar(RadialGradientBrush.RadiusY.WithValue(RelativeScalar(value, RelativeUnit.Relative)))

    /// <summary>Sets the RadiusY property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Radius value.</param>
    [<Extension>]
    static member inline radiusY(this: WidgetBuilder<'msg, #IFabRadialGradientBrush>, value: RelativeScalar) =
        this.AddScalar(RadialGradientBrush.RadiusY.WithValue(value))

    /// <summary>Link a ViewRef to access the direct RadialGradientBrush control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabRadialGradientBrush>, value: ViewRef<RadialGradientBrush>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
