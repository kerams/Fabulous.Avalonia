namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

type IFabDockPanel =
    inherit IFabPanel

module DockPanel =
    let WidgetKey = Widgets.register<DockPanel>()

    let Dock = Attributes.defineAvaloniaPropertyEnum DockPanel.DockProperty

    let LastChildFill =
        Attributes.defineAvaloniaPropertyBool DockPanel.LastChildFillProperty

    let HorizontalSpacing =
        Attributes.defineAvaloniaPropertyFloat DockPanel.HorizontalSpacingProperty

    let VerticalSpacing =
        Attributes.defineAvaloniaPropertyFloat DockPanel.VerticalSpacingProperty


[<AutoOpen>]
module DockPanelBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DockPanel widget.</summary>
        static member Dock() =
            let scalar = DockPanel.LastChildFill.WithValue(true)
            let attr = Panel.Children
            CollectionBuilder<'msg, IFabDockPanel, IFabControl>(DockPanel.WidgetKey, attr, scalar)

        /// <summary>Creates a DockPanel widget.</summary>
        /// <param name="lastChildFill">Whether the last child element within a DockPanel stretches to fill the remaining available space.</param>
        static member Dock(lastChildFill: bool) =
            let scalar = DockPanel.LastChildFill.WithValue(lastChildFill)
            let attr = Panel.Children
            CollectionBuilder<'msg, IFabDockPanel, IFabControl>(DockPanel.WidgetKey, attr, scalar)

type DockPanelModifiers =
    /// <summary>Sets the Dock property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Dock value.</param>
    [<Extension>]
    static member inline dock(this: WidgetBuilder<'msg, #IFabControl>, value: Dock) =
        this.AddScalar(DockPanel.Dock.WithValue(value))

    /// <summary>Sets the HorizontalSpacing property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The HorizontalSpacing value.</param>
    [<Extension>]
    static member inline horizontalSpacing(this: WidgetBuilder<'msg, #IFabDockPanel>, value: float) =
        this.AddScalar(DockPanel.HorizontalSpacing.WithValue(value))

    /// <summary>Sets the VerticalSpacing property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The VerticalSpacing value.</param>
    [<Extension>]
    static member inline verticalSpacing(this: WidgetBuilder<'msg, #IFabDockPanel>, value: float) =
        this.AddScalar(DockPanel.VerticalSpacing.WithValue(value))

    /// <summary>Link a ViewRef to access the direct DockPanel control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabDockPanel>, value: ViewRef<DockPanel>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
