namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module ComponentSplitButton =
    let Clicked =
        Attributes.Component.defineEvent "SplitButton_Clicked" (fun target -> (target :?> SplitButton).Click)

[<AutoOpen>]
module ComponentSplitButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        static member SplitButton(text: string, fn: RoutedEventArgs -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ComponentSplitButton.Clicked.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabSplitButton>(
                SplitButton.WidgetKey,
                &bundle
            )

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        /// <param name="content">The content to display in the flyout.</param>
        static member SplitButton(fn: RoutedEventArgs -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(ComponentSplitButton.Clicked.WithValue(fn)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabSplitButton>(SplitButton.WidgetKey, &bundle)
