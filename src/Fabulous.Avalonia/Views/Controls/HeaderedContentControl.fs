namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabHeaderedContentControl =
    inherit IFabContentControl

module HeaderedContentControl =
    let WidgetKey = Widgets.register<HeaderedContentControl>()

    let HeaderString =
        Attributes.defineAvaloniaProperty<string, obj> HeaderedContentControl.HeaderProperty box ScalarAttributeComparers.equalityCompare

    let HeaderWidget =
        Attributes.defineAvaloniaPropertyWidget HeaderedContentControl.HeaderProperty

[<AutoOpen>]
module HeaderedContentControlBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a HeaderedContentControl widget.</summary>
        /// <param name="header">The header string.</param>
        /// <param name="content">The content widget.</param>
        static member HeaderedContentControl(header: string, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(HeaderedContentControl.HeaderString.WithValue(header)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabHeaderedContentControl>(HeaderedContentControl.WidgetKey, &bundle)

        /// <summary>Creates a HeaderedContentControl widget.</summary>
        /// <param name="header">The header widget.</param>
        /// <param name="content">The content widget.</param>
        static member HeaderedContentControl(header: WidgetBuilder<'msg, #IFabControl>, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.empty(),
                    [| HeaderedContentControl.HeaderWidget.WithValue(header.Compile())
                       ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabHeaderedContentControl>(HeaderedContentControl.WidgetKey, &bundle)

        /// <summary>Creates a HeaderedContentControl widget.</summary>
        /// <param name="header">The header string.</param>
        /// <param name="content">The content string.</param>
        static member HeaderedContentControl(header: string, content: string) =
            let s1 = HeaderedContentControl.HeaderString.WithValue(header)
            let s2 = ContentControl.ContentString.WithValue(content)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabHeaderedContentControl>(
                HeaderedContentControl.WidgetKey,
                &bundle
            )

type HeaderedContentControlModifiers =

    /// <summary>Link a ViewRef to access the direct HeaderedContentControl control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabHeaderedContentControl>, value: ViewRef<HeaderedContentControl>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
