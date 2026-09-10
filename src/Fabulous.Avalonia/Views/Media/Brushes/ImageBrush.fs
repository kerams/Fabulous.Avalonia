namespace Fabulous.Avalonia

open System
open System.IO
open System.Runtime.CompilerServices
open Avalonia.Media
open Avalonia.Media.Imaging
open Fabulous

type IFabImageBrush =
    inherit IFabTileBrush

module ImageBrush =
    let WidgetKey = Widgets.register<ImageBrush>()

    let Source = Attributes.defineBindableImageSource ImageBrush.SourceProperty

[<AutoOpen>]
module ImageBrushBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ImageBrush widget.</summary>
        /// <param name="source">The image source.</param>
        static member ImageBrush(source: Bitmap) =
            let attr = ImageBrush.Source.WithValue(ImageSourceValue.Bitmap(source))
            WidgetBuilder<'msg, IFabImageBrush>(ImageBrush.WidgetKey, &attr)

        /// <summary>Creates a ImageBrush widget.</summary>
        /// <param name="source">The image source.</param>
        static member ImageBrush(source: string) =
            let attr = ImageBrush.Source.WithValue(ImageSourceValue.File(source))
            WidgetBuilder<'msg, IFabImageBrush>(ImageBrush.WidgetKey, &attr)

        /// <summary>Creates a ImageBrush widget.</summary>
        /// <param name="source">The image source.</param>
        static member ImageBrush(source: Uri) =
            let attr = ImageBrush.Source.WithValue(ImageSourceValue.Uri(source))
            WidgetBuilder<'msg, IFabImageBrush>(ImageBrush.WidgetKey, &attr)

        /// <summary>Creates a ImageBrush widget.</summary>
        /// <param name="source">The image source.</param>
        static member ImageBrush(source: Stream) =
            let attr = ImageBrush.Source.WithValue(ImageSourceValue.Stream(source))
            WidgetBuilder<'msg, IFabImageBrush>(ImageBrush.WidgetKey, &attr)

type ImageBrushModifiers =
    /// <summary>Link a ViewRef to access the direct ImageBrush control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabImageBrush>, value: ViewRef<ImageBrush>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
