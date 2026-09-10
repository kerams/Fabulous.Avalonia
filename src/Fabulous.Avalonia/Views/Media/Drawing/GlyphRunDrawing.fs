namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabGlyphRunDrawing =
    inherit IFabDrawing

module GlyphRunDrawing =
    let WidgetKey = Widgets.register<GlyphRunDrawing>()

    let ForegroundWidget =
        Attributes.defineAvaloniaPropertyWidget GlyphRunDrawing.ForegroundProperty

    let Foreground =
        Attributes.defineAvaloniaPropertyWithEquality GlyphRunDrawing.ForegroundProperty

    let GlyphRun =
        Attributes.defineAvaloniaPropertyWithEquality GlyphRunDrawing.GlyphRunProperty

[<AutoOpen>]
module GlyphRunDrawingBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a GlyphRunDrawing widget.</summary>
        /// <param name="brush">The content of the drawing.</param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        static member GlyphRunDrawing(brush: WidgetBuilder<'msg, #IFabBrush>, glyphRun: GlyphRun) =
            let bundle =
                AttributesBundle(
                    StackList.one(GlyphRunDrawing.GlyphRun.WithValue(glyphRun)),
                    [| GlyphRunDrawing.ForegroundWidget.WithValue(brush.Compile()) |],
                    [||]
                )
            WidgetBuilder<'msg, IFabGlyphRunDrawing>(
                GlyphRunDrawing.WidgetKey,
                &bundle
            )

        /// <summary>Creates a GlyphRunDrawing widget.</summary>
        /// <param name="brush">The content of the drawing.</param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        static member GlyphRunDrawing(brush: IBrush, glyphRun: GlyphRun) =
            let gr = GlyphRunDrawing.GlyphRun.WithValue(glyphRun)
            let fg = GlyphRunDrawing.Foreground.WithValue(brush)
            let bundle = AttributesBundle(StackList.two(gr, fg), [||], [||])
            WidgetBuilder<'msg, IFabGlyphRunDrawing>(GlyphRunDrawing.WidgetKey, &bundle)

        /// <summary>Creates a GlyphRunDrawing widget.</summary>
        /// <param name="brush">The content of the drawing.</param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        static member GlyphRunDrawing(brush: Color, glyphRun: GlyphRun) =
            View.GlyphRunDrawing(View.SolidColorBrush(brush), glyphRun)

        /// <summary>Creates a GlyphRunDrawing widget.</summary>
        /// <param name="brush">The content of the drawing.</param>
        /// <param name="glyphRun">The glyph run to draw.</param>
        static member GlyphRunDrawing(brush: string, glyphRun: GlyphRun) =
            View.GlyphRunDrawing(View.SolidColorBrush(brush), glyphRun)


type GlyphRunDrawingModifiers =

    /// <summary>Link a ViewRef to access the direct GlyphRunDrawing control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabGlyphRunDrawing>, value: ViewRef<GlyphRunDrawing>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
