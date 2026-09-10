namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Animation
open Avalonia.Controls
open Fabulous

type IFabToggleSwitch =
    inherit IFabToggleButton

module ToggleSwitch =
    let WidgetKey = Widgets.register<ToggleSwitch>()

    let OffContent =
        Attributes.defineAvaloniaProperty<string, obj> ToggleSwitch.OffContentProperty box ScalarAttributeComparers.equalityCompare

    let OffContentWidget =
        Attributes.defineAvaloniaPropertyWidget ToggleSwitch.OffContentProperty

    let OnContent =
        Attributes.defineAvaloniaProperty<string, obj> ToggleSwitch.OnContentProperty box ScalarAttributeComparers.equalityCompare

    let OnContentWidget =
        Attributes.defineAvaloniaPropertyWidget ToggleSwitch.OnContentProperty


    let KnobTransitions =
        Attributes.defineAvaloniaListWidgetCollection "ToggleSwitch_KnobTransitions" (fun target ->
            let target = (target :?> ToggleSwitch)

            if isNull target.Transitions then
                let newColl = Transitions()
                target.Transitions <- newColl
                newColl
            else
                target.Transitions)


type ToggleSwitchModifiers =
    /// <summary>Sets the OffContent property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OffContent value.</param>
    [<Extension>]
    static member inline offContent(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: string) =
        this.AddScalar(ToggleSwitch.OffContent.WithValue(value))

    /// <summary>Sets the OffContent property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OffContent value.</param>
    [<Extension>]
    static member inline offContent(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: WidgetBuilder<'msg, #IFabControl>) =
        let widget = ToggleSwitch.OffContentWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the OnContent property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OnContent value.</param>
    [<Extension>]
    static member inline onContent(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: string) =
        this.AddScalar(ToggleSwitch.OnContent.WithValue(value))

    /// <summary>Sets the OnContent property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OnContent value.</param>
    [<Extension>]
    static member inline onContent(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: WidgetBuilder<'msg, #IFabControl>) =
        let widget = ToggleSwitch.OnContentWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the Content property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Content value.</param>
    [<Extension>]
    static member inline content(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: string) =
        this.AddScalar(ContentControl.ContentString.WithValue(value))

    /// <summary>Sets the Content property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Content value.</param>
    [<Extension>]
    static member inline content(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: WidgetBuilder<'msg, #IFabControl>) =
        let widget = ContentControl.ContentWidget.WithValue(value.Compile())
        this.AddWidget(&widget)

    /// <summary>Sets the KnobTransitions property.</summary>
    /// <param name="this">Current widget.</param>
    [<Extension>]
    static member inline knobTransitions(this: WidgetBuilder<'msg, #IFabToggleSwitch>) =
        let attr = ToggleSwitch.KnobTransitions
        AttributeCollectionBuilder<'msg, #IFabToggleSwitch, IFabTransition>(&this, &attr)

    /// <summary>Sets the KnobTransitions property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The KnobTransitions value.</param>
    [<Extension>]
    static member inline knobTransition(this: WidgetBuilder<'msg, #IFabToggleSwitch>, value: WidgetBuilder<'msg, #IFabTransition>) =
        let attr = ToggleSwitch.KnobTransitions
        AttributeCollectionBuilder<'msg, #IFabToggleSwitch, IFabTransition>(&this, &attr) { value }

    /// <summary>Link a ViewRef to access the direct ToggleSwitch control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabToggleSwitch>, value: ViewRef<ToggleSwitch>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
