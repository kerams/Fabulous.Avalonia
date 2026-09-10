namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabScaleTransform =
    inherit IFabTransform

module ScaleTransform =

    let WidgetKey = Widgets.register<ScaleTransform>()

    let ScaleX =
        Attributes.defineAvaloniaPropertyWithEquality ScaleTransform.ScaleXProperty

    let ScaleY =
        Attributes.defineAvaloniaPropertyWithEquality ScaleTransform.ScaleYProperty

[<AutoOpen>]
module ScaleTransformBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ScaleTransform widget.</summary>
        /// <param name="scaleX">The X scale factor.</param>
        /// <param name="scaleY">The Y scale factor.</param>
        static member ScaleTransform(scaleX: float, scaleY: float) =
            let scaleXAttr = ScaleTransform.ScaleX.WithValue(scaleX)
            let scaleYAttr = ScaleTransform.ScaleY.WithValue(scaleY)
            WidgetBuilder<'msg, IFabScaleTransform>(ScaleTransform.WidgetKey, &scaleXAttr, &scaleYAttr)

        /// <summary>Creates a ScaleTransform widget.</summary>
        /// <param name="scaleX">The X scale factor.</param>
        static member ScaleTransform(scaleX: float) =
            let scaleXAttr = ScaleTransform.ScaleX.WithValue(scaleX)
            WidgetBuilder<'msg, IFabScaleTransform>(ScaleTransform.WidgetKey, &scaleXAttr)

        /// <summary>Creates a ScaleTransform widget.</summary>
        static member ScaleTransform() =
            WidgetBuilder<'msg, IFabScaleTransform>(ScaleTransform.WidgetKey)


type ScaleTransformModifiers =
    /// <summary>Link a ViewRef to access the direct ScaleTransform control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabScaleTransform>, value: ViewRef<ScaleTransform>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
