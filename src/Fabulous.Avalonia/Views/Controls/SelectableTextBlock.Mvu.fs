namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

module MvuSelectableTextBlock =
    let CopyingToClipboard =
        Attributes.Mvu.defineEvent "SelectableTextBlock_CopyingToClipboard" (fun target -> (target :?> SelectableTextBlock).CopyingToClipboard)

[<AutoOpen>]
module MvuSelectableTextBlockBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a SelectableTextBlock widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the user copies the text to the clipboard.</param>
        static member inline SelectableTextBlock(text: string, fn: RoutedEventArgs -> 'msg) =
            let s1 = TextBlock.Text.WithValue(text)
            let s2 = MvuSelectableTextBlock.CopyingToClipboard.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabSelectableTextBlock>(
                SelectableTextBlock.WidgetKey,
                &bundle
            )

        /// <summary>Creates a SelectableTextBlock widget.</summary>
        /// <param name="fn">Raised when the user copies the text to the clipboard.</param>
        static member inline SelectableTextBlock(fn: RoutedEventArgs -> 'msg) =
            let scalar = MvuSelectableTextBlock.CopyingToClipboard.WithValue(fn)
            let attr = TextBlock.Inlines
            CollectionBuilder<'msg, IFabSelectableTextBlock, IFabInline>(SelectableTextBlock.WidgetKey, attr, scalar)
