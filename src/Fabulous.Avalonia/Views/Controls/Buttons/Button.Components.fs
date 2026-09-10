namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module ComponentButton =
    let Clicked =
        Attributes.Component.defineEvent "Button_Clicked" (fun target -> (target :?> Button).Click)

[<AutoOpen>]
module ComponentButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Button widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the button is clicked.</param>
        static member Button(text: string, fn: RoutedEventArgs -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ComponentButton.Clicked.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabButton>(Button.WidgetKey, &bundle)

        /// <summary>Creates a Button widget.</summary>
        /// <param name="fn">Raised when the button is clicked.</param>
        /// <param name="content">The content to display.</param>
        static member Button(fn: RoutedEventArgs -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(ComponentButton.Clicked.WithValue(fn)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabButton>(Button.WidgetKey, &bundle)
