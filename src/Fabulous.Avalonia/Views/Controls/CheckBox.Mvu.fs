namespace Fabulous.Avalonia

open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module MvuCheckBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a CheckBox widget.</summary>
        /// <param name="isChecked">Whether the CheckBox is checked.</param>
        /// <param name="fn">Raised when the CheckBox is clicked.</param>
        static member CheckBox(isChecked: bool, fn: bool -> 'msg) =
            let attr = MvuToggleButton.CheckedChanged.WithValue(ValueEventData.create isChecked fn)
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &attr)

        /// <summary>Creates a CheckBox widget.</summary>
        /// <param name="text">The CheckBox text.</param>
        /// <param name="isChecked">Whether the CheckBox is checked.</param>
        /// <param name="fn">Raised when the CheckBox is clicked.</param>
        static member CheckBox(text: string, isChecked: bool, fn: bool -> 'msg) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = MvuToggleButton.CheckedChanged.WithValue(ValueEventData.create isChecked fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &bundle)

        /// <summary>Creates a CheckBox widget</summary>
        /// <param name="isChecked">Whether the CheckBox is checked.</param>
        /// <param name="fn">Raised when the CheckBox is clicked.</param>
        /// <param name="content">The CheckBox content.</param>
        static member CheckBox(isChecked: bool, fn: bool -> 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(
                StackList.one(MvuToggleButton.CheckedChanged.WithValue(ValueEventData.create isChecked fn)),
                [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                [||]
            )
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &bundle)

        /// <summary>Creates a ThreeStateCheckBox widget.</summary>
        /// <param name="isChecked">Whether the ThreeStateCheckBox is checked.</param>
        /// <param name="fn">Raised when the ThreeStateCheckBox is clicked.</param>
        static member inline ThreeStateCheckBox(isChecked: bool option, fn: bool option -> 'msg) =
            let s1 = ToggleButton.IsThreeState.WithValue(true)
            let s2 = MvuToggleButton.ThreeStateCheckedChanged.WithValue(ValueEventData.createVOption (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn))
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &bundle)

        /// <summary>Creates a ThreeStateCheckBox widget.</summary>
        /// <param name="text">The ThreeStateCheckBox text.</param>
        /// <param name="isChecked">Whether the ThreeStateCheckBox is checked.</param>
        /// <param name="fn">Raised when the ThreeStateCheckBox is clicked.</param>
        static member inline ThreeStateCheckBox(text: string, isChecked: bool option, fn: bool option -> 'msg) =
            let s1 = ToggleButton.IsThreeState.WithValue(true)
            let s2 = ContentControl.ContentString.WithValue(text)
            let s3 = MvuToggleButton.ThreeStateCheckedChanged.WithValue(ValueEventData.createVOption (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn))
            let bundle = AttributesBundle(StackList.three(s1, s2, s3), [||], [||])
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &bundle)

        /// <summary>Creates a ThreeStateCheckBox widget.</summary>
        /// <param name="isChecked">Whether the ThreeStateCheckBox is checked.</param>
        /// <param name="fn">Raised when the ThreeStateCheckBox is clicked.</param>
        /// <param name="content">The ThreeStateCheckBox content.</param>
        static member inline ThreeStateCheckBox(isChecked: bool option, fn: bool option -> 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(
                StackList.two(
                    MvuToggleButton.ThreeStateCheckedChanged.WithValue(
                        ValueEventData.createVOption (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn)
                    ),
                    ToggleButton.IsThreeState.WithValue(true)
                ),
                [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                [||]
            )
            WidgetBuilder<'msg, IFabCheckBox>(CheckBox.WidgetKey, &bundle)
