namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabBezierSegment =
    inherit IFabPathSegment

module BezierSegment =
    let WidgetKey = Widgets.register<BezierSegment>()

    let Point1 =
        Attributes.defineAvaloniaPropertyWithEquality BezierSegment.Point1Property

    let Point2 =
        Attributes.defineAvaloniaPropertyWithEquality BezierSegment.Point2Property

    let Point3 =
        Attributes.defineAvaloniaPropertyWithEquality BezierSegment.Point3Property

[<AutoOpen>]
module BezierSegmentBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a BezierSegment widget.</summary>
        /// <param name="point1">The first control point of the curve.</param>
        /// <param name="point2">The second control point of the curve.</param>
        /// <param name="point3">The third control point of the curve.</param>
        static member BezierSegment(point1: Point, point2: Point, point3: Point) =
            let p1 = BezierSegment.Point1.WithValue(point1)
            let p2 = BezierSegment.Point2.WithValue(point2)
            let p3 = BezierSegment.Point3.WithValue(point3)
            let bundle = AttributesBundle(StackList.three(p1, p2, p3), [||], [||])
            WidgetBuilder<'msg, IFabBezierSegment>(
                BezierSegment.WidgetKey,
                &bundle
            )


type BezierSegmentModifiers =

    /// <summary>Link a ViewRef to access the direct BezierSegment control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabBezierSegment>, value: ViewRef<BezierSegment>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
