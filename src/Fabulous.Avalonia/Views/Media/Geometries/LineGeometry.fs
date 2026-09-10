namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabLineGeometry =
    inherit IFabGeometry

module LineGeometry =
    let WidgetKey = Widgets.register<LineGeometry>()

    let StartPoint =
        Attributes.defineAvaloniaPropertyWithEquality LineGeometry.StartPointProperty

    let EndPoint =
        Attributes.defineAvaloniaPropertyWithEquality LineGeometry.EndPointProperty

[<AutoOpen>]
module ComponentLineGeometryBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a LineGeometry widget.</summary>
        /// <param name="startPoint">The start point of the line.</param>
        /// <param name="endPoint">The end point of the line.</param>
        static member LineGeometry(startPoint: Point, endPoint: Point) =
            let s1 = LineGeometry.StartPoint.WithValue(startPoint)
            let s2 = LineGeometry.EndPoint.WithValue(endPoint)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabLineGeometry>(
                LineGeometry.WidgetKey,
                &bundle
            )

type LineGeometryModifiers =

    /// <summary>Link a ViewRef to access the direct LineGeometry control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabLineGeometry>, value: ViewRef<LineGeometry>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
