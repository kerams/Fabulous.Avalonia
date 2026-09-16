namespace Fabulous.Avalonia

open System.IO
open System.Runtime.CompilerServices
open Avalonia.Media.Imaging
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Avalonia.Controls

type IFabTrayIcon =
    inherit IFabElement

module TrayIcon =
    let WidgetKey = Widgets.register<TrayIcon>()

    let Menu = Attributes.defineAvaloniaPropertyWidget TrayIcon.MenuProperty

    let IconSource = Attributes.defineBindableWindowIconSource TrayIcon.IconProperty

    let ToolTipText =
        Attributes.defineAvaloniaPropertyWithEquality TrayIcon.ToolTipTextProperty

    let IsVisible =
        Attributes.defineAvaloniaPropertyBool TrayIcon.IsVisibleProperty

[<AutoOpen>]
module TrayIconBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        static member TrayIcon(icon: Bitmap) =
            let attr = TrayIcon.IconSource.WithValue(ImageSourceValue.Bitmap(icon))
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &attr)

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        /// <param name="text">The tooltip text to display.</param>
        static member TrayIcon(icon: Bitmap, text: string) =
            let s1 = TrayIcon.IconSource.WithValue(ImageSourceValue.Bitmap(icon))
            let s2 = TrayIcon.ToolTipText.WithValue(text)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &bundle)

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        static member TrayIcon(icon: string) =
            let attr = TrayIcon.IconSource.WithValue(ImageSourceValue.File(icon))
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &attr)

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        /// <param name="text">The tooltip text to display.</param>
        static member TrayIcon(icon: string, text: string) =
            let s1 = TrayIcon.IconSource.WithValue(ImageSourceValue.File(icon))
            let s2 = TrayIcon.ToolTipText.WithValue(text)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &bundle)

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        static member TrayIcon(icon: Stream) =
            let attr = TrayIcon.IconSource.WithValue(ImageSourceValue.Stream(icon))
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &attr)

        /// <summary>Creates a TrayIcon widget.</summary>
        /// <param name="icon">The icon to display.</param>
        /// <param name="text">The tooltip text to display.</param>
        static member TrayIcon(icon: Stream, text: string) =
            let s1 = TrayIcon.IconSource.WithValue(ImageSourceValue.Stream(icon))
            let s2 = TrayIcon.ToolTipText.WithValue(text)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabTrayIcon>(TrayIcon.WidgetKey, &bundle)

type TrayIconModifiers =
    /// <summary>Sets the Menu property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Menu value.</param>
    [<Extension>]
    static member inline menu(this: WidgetBuilder<'msg, #IFabTrayIcon>, value: WidgetBuilder<'msg, #IFabNativeMenu>) =
        let widget = TrayIcon.Menu.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the IsVisible property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsVisible value.</param>
    [<Extension>]
    static member inline isVisible(this: WidgetBuilder<'msg, #IFabTrayIcon>, value: bool) =
        this.AddScalar(TrayIcon.IsVisible.WithValue(value))

    /// <summary>Link a ViewRef to access the direct TrayIcon control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTrayIcon>, value: ViewRef<TrayIcon>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
