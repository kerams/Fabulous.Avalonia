namespace Fabulous.Avalonia

open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module ComponentRepeatButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a RepeatButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="msg">Raised when the button is clicked.</param>
        static member RepeatButton(text: string, msg: RoutedEventArgs -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ComponentButton.Clicked.WithValue(msg)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabRepeatButton>(RepeatButton.WidgetKey, &bundle)

        /// <summary>Creates a RepeatButton widget.</summary>
        /// <param name="content">The content to display.</param>
        /// M<param name="fn">Raised when the button is clicked.</param>
        static member RepeatButton(fn: RoutedEventArgs -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(ComponentButton.Clicked.WithValue(fn)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabRepeatButton>(RepeatButton.WidgetKey, &bundle)
