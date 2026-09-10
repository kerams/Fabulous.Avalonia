namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabQuadraticBezierSegment =
    inherit IFabPathSegment

module QuadraticBezierSegment =
    let WidgetKey = Widgets.register<QuadraticBezierSegment>()

    let Point1 =
        Attributes.defineAvaloniaPropertyWithEquality QuadraticBezierSegment.Point1Property

    let Point2 =
        Attributes.defineAvaloniaPropertyWithEquality QuadraticBezierSegment.Point2Property

[<AutoOpen>]
module QuadraticBezierSegmentBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a QuadraticBezierSegment widget.</summary>
        /// <param name="point1">The first control point of the curve.</param>
        /// <param name="point2">The second control point of the curve.</param>
        static member QuadraticBezierSegment(point1: Point, point2: Point) =
            let s1 = QuadraticBezierSegment.Point1.WithValue(point1)
            let s2 = QuadraticBezierSegment.Point2.WithValue(point2)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabQuadraticBezierSegment>(
                QuadraticBezierSegment.WidgetKey,
                &bundle
            )


type QuadraticBezierSegmentModifiers =

    /// <summary>Link a ViewRef to access the direct QuadraticBezierSegment control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabQuadraticBezierSegment>, value: ViewRef<QuadraticBezierSegment>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
