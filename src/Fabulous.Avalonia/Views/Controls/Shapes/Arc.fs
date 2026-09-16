namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Shapes
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabArc =
    inherit IFabShape

module Arc =
    let WidgetKey = Widgets.register<Arc>()

    let StartAngle =
        Attributes.defineAvaloniaPropertyFloat Arc.StartAngleProperty

    let SweepAngle =
        Attributes.defineAvaloniaPropertyFloat Arc.SweepAngleProperty

[<AutoOpen>]
module ArcBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Arc widget/</summary>
        /// <param name="startAngle">The starting angle/</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        static member Arc(startAngle: float, sweepAngle: float) =
            let s1 = Arc.StartAngle.WithValue(startAngle)
            let s2 = Arc.SweepAngle.WithValue(sweepAngle)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabArc>(Arc.WidgetKey, &bundle)

type ArcModifiers =
    /// <summary>Link a ViewRef to access the direct Arc control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabArc>, value: ViewRef<Arc>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
