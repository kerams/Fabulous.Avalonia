namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabComboBoxItem =
    inherit IFabListBoxItem

module ComboBoxItem =
    let WidgetKey = Widgets.register<ComboBoxItem>()

[<AutoOpen>]
module ComboBoxItemBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ComboBoxItem widget.</summary>
        /// <param name="content">The content of the ComboBoxItem.</param>
        static member ComboBoxItem(content: string) =
            let attr = ContentControl.ContentString.WithValue(content)
            WidgetBuilder<'msg, IFabComboBoxItem>(ComboBoxItem.WidgetKey, &attr)

        /// <summary>Creates a ComboBoxItem widget.</summary>
        /// <param name="content">The content of the ComboBoxItem.</param>
        static member ComboBoxItem(content: WidgetBuilder<'msg, #IFabControl>) =
            let widget = ContentControl.ContentWidget.WithValue(content.Compile())
            WidgetBuilder<'msg, IFabComboBoxItem>(ComboBoxItem.WidgetKey, &widget)

type ComboBoxItemModifiers =
    /// <summary>Link a ViewRef to access the direct MenuItem control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComboBoxItem>, value: ViewRef<ComboBoxItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
