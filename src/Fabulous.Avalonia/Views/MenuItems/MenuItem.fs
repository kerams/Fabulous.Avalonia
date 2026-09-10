namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Avalonia.Input
open Fabulous
open Fabulous.StackAllocatedCollections
open Fabulous.StackAllocatedCollections.StackList

type IFabMenuItem =
    inherit IFabHeaderedSelectingItemsControl

module MenuItem =
    let WidgetKey = Widgets.register<MenuItem>()

    let HotKey = Attributes.defineAvaloniaPropertyWithEquality MenuItem.HotKeyProperty

    let Icon = Attributes.defineAvaloniaPropertyWidget MenuItem.IconProperty

    let InputGesture =
        Attributes.defineAvaloniaPropertyWithEquality MenuItem.InputGestureProperty

    let StaysOpenOnClick =
        Attributes.defineAvaloniaPropertyWithEquality MenuItem.StaysOpenOnClickProperty

    let IsSubMenuOpen =
        Attributes.defineAvaloniaPropertyWithEquality MenuItem.IsSubMenuOpenProperty

[<AutoOpen>]
module MenuItemBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a MenuItem widget.</summary>
        /// <param name="header">The header of the menu item.</param>
        static member MenuItem(header: string) =
            let attr = HeaderedContentControl.HeaderString.WithValue(header)
            WidgetBuilder<'msg, IFabMenuItem>(MenuItem.WidgetKey, &attr)

        /// <summary>Creates a MenuItem widget.</summary>
        /// <param name="header">The header of the menu item.</param>
        static member MenuItem(header: WidgetBuilder<'msg, #IFabControl>) =
            let widget = HeaderedContentControl.HeaderWidget.WithValue(header.Compile())
            WidgetBuilder<'msg, IFabMenuItem>(MenuItem.WidgetKey, &widget)

[<AutoOpen>]
module MenuItemsBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a MenuItems widget.</summary>
        static member MenuItems() =
            let attr = ItemsControl.Items
            CollectionBuilder<'msg, IFabMenuItem, IFabMenuItem>(MenuItem.WidgetKey, attr)

        /// <summary>Creates a MenuItems widget.</summary>
        static member MenuItems(header: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.empty(), [| HeaderedContentControl.HeaderWidget.WithValue(header.Compile()) |], [||])
            let attr = ItemsControl.Items
            CollectionBuilder<'msg, IFabMenuItem, IFabMenuItem>(MenuItem.WidgetKey, attr, bundle)

        /// <summary>Creates a MenuItems widget.</summary>
        /// <param name="header">The header of the menu item.</param>
        static member MenuItems(header: string) =
            let scalar = HeaderedContentControl.HeaderString.WithValue(header)
            let attr = ItemsControl.Items
            CollectionBuilder<'msg, IFabMenuItem, IFabMenuItem>(MenuItem.WidgetKey, attr, scalar)

type MenuItemModifiers =
    /// <summary>Sets the HotKey property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The HotKey value.</param>
    [<Extension>]
    static member inline hotKey(this: WidgetBuilder<'msg, #IFabMenuItem>, value: KeyGesture) =
        this.AddScalar(MenuItem.HotKey.WithValue(value))

    /// <summary>Sets the Icon property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Icon value.</param>
    [<Extension>]
    static member inline icon(this: WidgetBuilder<'msg, #IFabMenuItem>, value: WidgetBuilder<'msg, #IFabControl>) =
        let widget = MenuItem.Icon.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the InputGesture property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The InputGesture value.</param>
    [<Extension>]
    static member inline inputGesture(this: WidgetBuilder<'msg, #IFabMenuItem>, value: KeyGesture) =
        this.AddScalar(MenuItem.InputGesture.WithValue(value))

    /// <summary>Sets the StaysOpenOnClick property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The StaysOpenOnClick value.</param>
    [<Extension>]
    static member inline staysOpenOnClick(this: WidgetBuilder<'msg, #IFabMenuItem>, value: bool) =
        this.AddScalar(MenuItem.StaysOpenOnClick.WithValue(value))

    /// <summary>Sets the IsSubMenuOpen property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsSubMenuOpen value.</param>
    [<Extension>]
    static member inline isSubMenuOpen(this: WidgetBuilder<'msg, #IFabMenuItem>, value: bool) =
        this.AddScalar(MenuItem.IsSubMenuOpen.WithValue(value))

    /// <summary>Link a ViewRef to access the direct MenuItem control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabMenuItem>, value: ViewRef<MenuItem>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

type MenuItemCollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'itemType :> IFabMenuItem>
        (_: CollectionBuilder<'msg, 'marker, IFabMenuItem>, x: WidgetBuilder<'msg, 'itemType>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'itemType :> IFabMenuItem>
        (_: CollectionBuilder<'msg, 'marker, IFabMenuItem>, x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
