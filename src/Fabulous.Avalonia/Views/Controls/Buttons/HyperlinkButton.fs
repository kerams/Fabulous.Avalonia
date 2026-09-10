namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabHyperlinkButton =
    inherit IFabButton

module HyperlinkButton =
    let WidgetKey = Widgets.register<HyperlinkButton>()

    let NavigateUri =
        Attributes.defineAvaloniaPropertyWithEquality HyperlinkButton.NavigateUriProperty

    let IsVisited =
        Attributes.defineAvaloniaPropertyWithEquality HyperlinkButton.IsVisitedProperty

[<AutoOpen>]
module HyperlinkButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a HyperlinkButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="uri">The Uri to navigate to when the HyperlinkButton is clicked.</param>
        static member HyperlinkButton(text: string, uri: Uri) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = HyperlinkButton.NavigateUri.WithValue(uri)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabHyperlinkButton>(
                HyperlinkButton.WidgetKey,
                &bundle
            )

        /// <summary>Creates a HyperlinkButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="uri">The Uri to navigate to when the HyperlinkButton is clicked.</param>
        static member HyperlinkButton(text: string, uri: string) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = HyperlinkButton.NavigateUri.WithValue(Uri(uri))
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabHyperlinkButton>(HyperlinkButton.WidgetKey, &bundle)

        /// <summary>Creates a HyperlinkButton widget.</summary>
        /// <param name="uri">The Uri to navigate to when the HyperlinkButton is clicked.</param>
        /// <param name="content">The content of the HyperlinkButton.</param>
        static member HyperlinkButton(uri: Uri, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(HyperlinkButton.NavigateUri.WithValue(uri)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabHyperlinkButton>(HyperlinkButton.WidgetKey, &bundle)

        /// <summary>Creates a HyperlinkButton widget.</summary>
        /// <param name="uri">The Uri to navigate to when the HyperlinkButton is clicked.</param>
        /// <param name="content">The content of the HyperlinkButton.</param>
        static member HyperlinkButton(uri: string, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(HyperlinkButton.NavigateUri.WithValue(Uri(uri))),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabHyperlinkButton>(HyperlinkButton.WidgetKey, &bundle)

type HyperlinkButtonModifiers =

    /// <summary>Sets the IsVisited  property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsVisited  value.</param>
    [<Extension>]
    static member inline isVisited(this: WidgetBuilder<'msg, IFabHyperlinkButton>, value: bool) =
        this.AddScalar(HyperlinkButton.IsVisited.WithValue(value))

    /// <summary>Link a ViewRef to access the direct HyperlinkButton control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabHyperlinkButton>, value: ViewRef<HyperlinkButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
