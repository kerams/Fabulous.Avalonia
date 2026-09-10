namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module MvuButton =
    let Clicked =
        Attributes.Mvu.defineEvent "Button_Clicked" (fun target -> (target :?> Button).Click)

[<AutoOpen>]
module MvuButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Button widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the button is clicked.</param>
        static member Button(text: string, fn: 'msg) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = MvuButton.Clicked.WithValue(fun _ -> fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabButton>(Button.WidgetKey, &bundle)

        /// <summary>Creates a Button widget.</summary>
        /// <param name="fn">Raised when the button is clicked.</param>
        /// <param name="content">The content to display.</param>
        static member Button(fn: 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(MvuButton.Clicked.WithValue(fun _ -> fn)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabButton>(Button.WidgetKey, &bundle)
