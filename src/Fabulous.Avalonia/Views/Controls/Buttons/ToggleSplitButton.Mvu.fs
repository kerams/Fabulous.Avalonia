namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module MvuToggleSplitButton =
    let CheckedChanged =
        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "ToggleSplitButton_CheckedChanged" ToggleSplitButton.IsCheckedProperty

[<AutoOpen>]
module MvuToggleSplitButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ToggleSplitButton widget.</summary>
        /// <param name="text">The text of the ToggleSplitButton.</param>
        /// <param name="isChecked">Whether the ToggleSplitButton is checked.</param>
        /// <param name="fn">Raised when the ToggleSplitButton is checked or unchecked.</param>
        static member ToggleSplitButton(text: string, isChecked: bool, fn: bool -> 'msg) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = MvuToggleSplitButton.CheckedChanged.WithValue(ValueEventData.create isChecked fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabToggleSplitButton>(ToggleSplitButton.WidgetKey, &bundle)

        /// <summary>Creates a ToggleSplitButton widget.</summary>
        /// <param name="isChecked">Whether the ToggleSplitButton is checked.</param>
        /// <param name="fn">Raised when the ToggleSplitButton is checked or unchecked.</param>
        /// <param name="content">The content of the ToggleSplitButton.</param>
        static member ToggleSplitButton(isChecked: bool, fn: bool -> 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(
                StackList.one(MvuToggleSplitButton.CheckedChanged.WithValue(ValueEventData.create isChecked fn)),
                [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                [||]
            )
            WidgetBuilder<'msg, IFabToggleSplitButton>(ToggleSplitButton.WidgetKey, &bundle)
