namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

module ComponentNativeMenuItem =

    let Click =
        Attributes.Component.defineEventNoArg "NativeMenuItem_Click" (fun target -> (target :?> NativeMenuItem).Click)

[<AutoOpen>]
module ComponentNativeMenuItemBuilders =
    type Fabulous.Avalonia.View with
        /// <summary>Creates a NativeMenuItem widget.</summary>
        /// <param name="header">The header of the Flyout.</param>
        /// <param name="onClicked">Raised when the menu item is clicked.</param>
        static member NativeMenuItem(header: string, onClicked: unit -> unit) =
            let s1 = NativeMenuItem.Header.WithValue(header)
            let s2 = ComponentNativeMenuItem.Click.WithValue(onClicked)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabNativeMenuItem>(
                NativeMenuItem.WidgetKey,
                &bundle
            )
