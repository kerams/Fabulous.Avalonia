namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Input
open Avalonia.Media
open Avalonia.Styling
open Fabulous

type IFabTopLevel =
    inherit IFabContentControl

module TopLevel =
    let TransparencyLevelHint =
        Attributes.defineAvaloniaPropertyWithEquality TopLevel.TransparencyLevelHintProperty

    let SystemBarColorWidget =
        Attributes.defineAvaloniaPropertyWidget TopLevel.SystemBarColorProperty

    let SystemBarColor =
        Attributes.defineAvaloniaPropertyWithEquality TopLevel.SystemBarColorProperty

    let TransparencyBackgroundFallbackWidget =
        Attributes.defineAvaloniaPropertyWidget TopLevel.TransparencyBackgroundFallbackProperty

    let TransparencyBackgroundFallback =
        Attributes.defineAvaloniaPropertyWithEquality TopLevel.TransparencyBackgroundFallbackProperty

    let AutoSafeAreaPadding =
        Attributes.defineAvaloniaPropertyWithEquality TopLevel.AutoSafeAreaPaddingProperty

    let RequestedThemeVariant =
        Attributes.definePropertyWithGetSet
            "TopLevel_RequestedThemeVariant"
            (fun target ->
                let target = target :?> TopLevel
                target.ActualThemeVariant)
            (fun target value ->
                let target = target :?> TopLevel
                target.RequestedThemeVariant <- value)

type TopLevelModifiers =
    /// <summary>Sets the ThemeVariant property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ThemeVariant value.</param>
    [<Extension>]
    static member inline requestedThemeVariant(this: WidgetBuilder<'msg, #IFabTopLevel>, value: ThemeVariant) =
        this.AddScalar(TopLevel.RequestedThemeVariant.WithValue(value))

    /// <summary>Sets the TransparencyLevelHint property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The TransparencyLevelHint value.</param>
    [<Extension>]
    static member inline transparencyLevelHint(this: WidgetBuilder<'msg, #IFabTopLevel>, value: WindowTransparencyLevel list) =
        this.AddScalar(TopLevel.TransparencyLevelHint.WithValue(value))

    /// <summary>Sets the TransparencyBackgroundFallbackWidget property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The TransparencyBackgroundFallbackWidget value.</param>
    [<Extension>]
    static member inline transparencyBackgroundFallback(this: WidgetBuilder<'msg, #IFabTopLevel>, value: WidgetBuilder<'msg, #IFabBrush>) =
        let widget = TopLevel.TransparencyBackgroundFallbackWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the TransparencyBackgroundFallback property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The TransparencyBackgroundFallback value.</param>
    [<Extension>]
    static member inline transparencyBackgroundFallback(this: WidgetBuilder<'msg, #IFabTopLevel>, value: IBrush) =
        this.AddScalar(TopLevel.TransparencyBackgroundFallback.WithValue(value))

    /// <summary>Sets the SystemBarColorWidget property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The SystemBarColorWidget value.</param>
    [<Extension>]
    static member inline systemBarColor(this: WidgetBuilder<'msg, #IFabTopLevel>, value: WidgetBuilder<'msg, #IFabBrush>) =
        let widget = TopLevel.SystemBarColorWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the AutoSafeAreaPadding property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The AutoSafeAreaPadding value.</param>
    [<Extension>]
    static member inline autoSafeAreaPadding(this: WidgetBuilder<'msg, #IFabTopLevel>, value: bool) =
        this.AddScalar(TopLevel.AutoSafeAreaPadding.WithValue(value))

    /// <summary>Sets the TransparencyBackgroundFallback property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The TransparencyBackgroundFallback value.</param>
    [<Extension>]
    static member inline transparencyBackgroundFallback(this: WidgetBuilder<'msg, #IFabTopLevel>, value: Color) =
        let brush: WidgetBuilder<'msg, IFabSolidColorBrush> = View.SolidColorBrush(value)
        TopLevelModifiers.transparencyBackgroundFallback(this, brush)

    /// <summary>Sets the TransparencyBackgroundFallback property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The TransparencyBackgroundFallback value.</param>
    [<Extension>]
    static member inline transparencyBackgroundFallback(this: WidgetBuilder<'msg, #IFabTopLevel>, value: string) =
        let brush: WidgetBuilder<'msg, IFabSolidColorBrush> = View.SolidColorBrush(value)
        TopLevelModifiers.transparencyBackgroundFallback(this, brush)

    /// <summary>Sets the SystemBarColor property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The SystemBarColor value.</param>
    [<Extension>]
    static member inline systemBarColor(this: WidgetBuilder<'msg, #IFabTopLevel>, value: Color) =
        let brush: WidgetBuilder<'msg, IFabSolidColorBrush> = View.SolidColorBrush(value)
        TopLevelModifiers.systemBarColor(this, brush)

    /// <summary>Sets the SystemBarColor property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The SystemBarColor value.</param>
    [<Extension>]
    static member inline systemBarColor(this: WidgetBuilder<'msg, #IFabTopLevel>, value: string) =
        let brush: WidgetBuilder<'msg, IFabSolidColorBrush> = View.SolidColorBrush(value)
        TopLevelModifiers.systemBarColor(this, brush)
