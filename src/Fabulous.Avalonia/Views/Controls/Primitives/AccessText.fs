namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabAccessText =
    inherit IFabTextBlock

module AccessText =
    let WidgetKey = Widgets.register<AccessText>()

    let ShowAccessKey =
        Attributes.defineAvaloniaPropertyBool AccessText.ShowAccessKeyProperty

[<AutoOpen>]
module AccessTextBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a AccessText widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="showAccessKey">Whether to underline the access key in the text.</param>
        static member inline AccessText(text: string, showAccessKey: bool) =
            let s1 = TextBlock.Text.WithValue(text)
            let s2 = AccessText.ShowAccessKey.WithValue(showAccessKey)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabAccessText>(AccessText.WidgetKey, &bundle)

type AccessTextModifiers =
    /// <summary>Link a ViewRef to access the direct AccessText control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabAccessText>, value: ViewRef<AccessText>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
