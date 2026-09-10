namespace Fabulous.Avalonia

open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module ComponentDropDownButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DropDownButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="msg">Raised when the DropDownButton is clicked.</param>
        static member DropDownButton(text: string, msg: RoutedEventArgs -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ComponentButton.Clicked.WithValue(msg)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabDropDownButton>(
                DropDownButton.WidgetKey,
                &bundle
            )

        /// <summary>Creates a DropDownButton widget.</summary>
        /// <param name="msg">Raised when the DropDownButton is clicked.</param>
        /// <param name="content">The content of the DropDownButton.</param>
        static member DropDownButton(msg: RoutedEventArgs -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(ComponentButton.Clicked.WithValue(msg)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabDropDownButton>(DropDownButton.WidgetKey, &bundle)
