namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

module MvuNativeMenuItem =

    let Click =
        Attributes.Mvu.defineEventNoArg "NativeMenuItem_Click" (fun target -> (target :?> NativeMenuItem).Click)

[<AutoOpen>]
module MvuNativeMenuItemBuilders =
    type Fabulous.Avalonia.View with
        /// <summary>Creates a NativeMenuItem widget.</summary>
        /// <param name="header">The header of the Flyout.</param>
        /// <param name="onClicked">Raised when the menu item is clicked.</param>
        static member NativeMenuItem(header: string, onClicked: 'msg) =
            let s1 = NativeMenuItem.Header.WithValue(header)
            let s2 = MvuNativeMenuItem.Click.WithValue(MsgValue onClicked)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabNativeMenuItem>(
                NativeMenuItem.WidgetKey,
                &bundle
            )
