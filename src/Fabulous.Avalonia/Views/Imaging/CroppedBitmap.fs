namespace Fabulous.Avalonia

open System
open System.IO
open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media.Imaging
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabCroppedBitmap =
    inherit IFabElement

module CroppedBitmap =

    let WidgetKey = Widgets.register<CroppedBitmap>()

    let Source = Attributes.defineBindableImageSource CroppedBitmap.SourceProperty

    let SourceRect =
        Attributes.defineAvaloniaPropertyWithEquality CroppedBitmap.SourceRectProperty

[<AutoOpen>]
module CroppedBitmapBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a CroppedBitmap widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="rect">The rectangular area that the bitmap is cropped to.</param>
        static member CroppedBitmap(source: Bitmap, rect: PixelRect) =
            let s1 = CroppedBitmap.Source.WithValue(ImageSourceValue.Bitmap(source))
            let s2 = CroppedBitmap.SourceRect.WithValue(rect)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCroppedBitmap>(CroppedBitmap.WidgetKey, &bundle)

        /// <summary>Creates a CroppedBitmap widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="rect">The rectangular area that the bitmap is cropped to.</param>
        static member CroppedBitmap(source: string, rect: PixelRect) =
            let s1 = CroppedBitmap.Source.WithValue(ImageSourceValue.File(source))
            let s2 = CroppedBitmap.SourceRect.WithValue(rect)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCroppedBitmap>(CroppedBitmap.WidgetKey, &bundle)

        /// <summary>Creates a CroppedBitmap widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="rect">The rectangular area that the bitmap is cropped to.</param>
        static member CroppedBitmap(source: Uri, rect: PixelRect) =
            let s1 = CroppedBitmap.Source.WithValue(ImageSourceValue.Uri(source))
            let s2 = CroppedBitmap.SourceRect.WithValue(rect)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCroppedBitmap>(CroppedBitmap.WidgetKey, &bundle)

        /// <summary>Creates a CroppedBitmap widget.</summary>
        /// <param name="source">The source image.</param>
        /// <param name="rect">The rectangular area that the bitmap is cropped to.</param>
        static member CroppedBitmap(source: Stream, rect: PixelRect) =
            let s1 = CroppedBitmap.Source.WithValue(ImageSourceValue.Stream(source))
            let s2 = CroppedBitmap.SourceRect.WithValue(rect)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCroppedBitmap>(CroppedBitmap.WidgetKey, &bundle)

type CroppedBitmapModifiers =
    /// <summary>Link a ViewRef to access the direct CroppedBitmap control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabCroppedBitmap>, value: ViewRef<CroppedBitmap>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
