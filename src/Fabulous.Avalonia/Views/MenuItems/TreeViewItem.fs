namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Fabulous

type IFabTreeViewItem =
    inherit IFabHeaderedItemsControl

module TreeViewItem =
    let WidgetKey = Widgets.register<TreeViewItem>()

    let IsExpanded =
        Attributes.defineAvaloniaPropertyBool TreeViewItem.IsExpandedProperty

[<AutoOpen>]
module TreeViewItemBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TreeViewItem widget.</summary>
        /// <param name="content">The content of the TreeViewItem.</param>
        static member TreeViewItem(content: string) =
            let attr = HeaderedItemsControl.HeaderString.WithValue(content)
            WidgetBuilder<'msg, IFabTreeViewItem>(TreeViewItem.WidgetKey, &attr)

        /// <summary>Creates a TreeViewItem widget.</summary>
        /// <param name="content">The content of the TreeViewItem.</param>
        static member TreeViewItem(content: WidgetBuilder<'msg, #IFabControl>) =
            let widget = HeaderedItemsControl.HeaderWidget.WithValue(content.Compile())
            WidgetBuilder<'msg, IFabTreeViewItem>(TreeViewItem.WidgetKey, &widget)

type TreeViewItemModifiers =

    /// <summary>Sets the IsExpanded property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsExpanded value.</param>
    [<Extension>]
    static member inline isExpanded(this: WidgetBuilder<'msg, #IFabTreeViewItem>, value: bool) =
        this.AddScalar(TreeViewItem.IsExpanded.WithValue(value))

    /// <summary>Link a ViewRef to access the direct TreeViewItem control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTreeViewItem>, value: ViewRef<TreeViewItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
