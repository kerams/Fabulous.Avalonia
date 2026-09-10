namespace Fabulous.Avalonia

open System
open System.IO
open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Media
open Avalonia.Media.Imaging
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabImage =
    inherit IFabControl

module Image =
    let WidgetKey = Widgets.register<Image>()

    let Source = Attributes.defineBindableImageSource Image.SourceProperty

    let SourceWidget = Attributes.defineAvaloniaPropertyWidget Image.SourceProperty

    let Stretch = Attributes.defineAvaloniaPropertyWithEquality Image.StretchProperty

    let StretchDirection =
        Attributes.defineAvaloniaPropertyWithEquality Image.StretchDirectionProperty

    let BlendMode =
        Attributes.defineAvaloniaPropertyWithEquality Image.BlendModeProperty

[<AutoOpen>]
module ImageBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: Bitmap) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Bitmap(source))
            let s2 = Image.Stretch.WithValue(Stretch.Uniform)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(source: Bitmap, stretch: Stretch) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Bitmap(source))
            let s2 = Image.Stretch.WithValue(stretch)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: string) =
            let s1 = Image.Source.WithValue(ImageSourceValue.File(source))
            let s2 = Image.Stretch.WithValue(Stretch.Uniform)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(source: string, stretch: Stretch) =
            let s1 = Image.Source.WithValue(ImageSourceValue.File(source))
            let s2 = Image.Stretch.WithValue(stretch)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: Uri) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Uri(source))
            let s2 = Image.Stretch.WithValue(Stretch.Uniform)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(source: Uri, stretch: Stretch) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Uri(source))
            let s2 = Image.Stretch.WithValue(stretch)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: Stream) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Stream(source))
            let s2 = Image.Stretch.WithValue(Stretch.Uniform)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(source: Stream, stretch: Stretch) =
            let s1 = Image.Source.WithValue(ImageSourceValue.Stream(source))
            let s2 = Image.Stretch.WithValue(stretch)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: WidgetBuilder<'msg, #IFabDrawingImage>) =
            let bundle = AttributesBundle(StackList.one(Image.Stretch.WithValue(Stretch.Uniform)), [| Image.SourceWidget.WithValue(source.Compile()) |], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(stretch: Stretch, source: WidgetBuilder<'msg, #IFabDrawingImage>) =
            let bundle = AttributesBundle(StackList.one(Image.Stretch.WithValue(stretch)), [| Image.SourceWidget.WithValue(source.Compile()) |], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        static member Image(source: WidgetBuilder<'msg, IFabCroppedBitmap>) =
            let bundle = AttributesBundle(StackList.one(Image.Stretch.WithValue(Stretch.Uniform)), [| Image.SourceWidget.WithValue(source.Compile()) |], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

        /// <summary>Creates an Image widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="stretch">The stretch mode.</param>
        static member Image(stretch: Stretch, source: WidgetBuilder<'msg, IFabCroppedBitmap>) =
            let bundle = AttributesBundle(StackList.one(Image.Stretch.WithValue(stretch)), [| Image.SourceWidget.WithValue(source.Compile()) |], [||])
            WidgetBuilder<'msg, IFabImage>(Image.WidgetKey, &bundle)

type ImageModifiers =
    /// <summary>Sets the StretchDirection property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The StretchDirection value.</param>
    [<Extension>]
    static member inline stretchDirection(this: WidgetBuilder<'msg, #IFabImage>, value: StretchDirection) =
        this.AddScalar(Image.StretchDirection.WithValue(value))

    /// <summary>Sets the BlendMode property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The BlendMode value.</param>
    [<Extension>]
    static member inline blendMode(this: WidgetBuilder<'msg, #IFabImage>, value: BitmapBlendingMode) =
        this.AddScalar(Image.BlendMode.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Image control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabImage>, value: ViewRef<Image>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
