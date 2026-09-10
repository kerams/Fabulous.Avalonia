namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Layout
open Fabulous
open System.Runtime.CompilerServices
open Fabulous.StackAllocatedCollections.StackList

type IFabReversibleStackPanel =
    inherit IFabStackPanel

module ReversibleStackPanel =
    let WidgetKey = Widgets.register<ReversibleStackPanel>()

    let ReverseOrder =
        Attributes.defineAvaloniaPropertyWithEquality ReversibleStackPanel.ReverseOrderProperty

[<AutoOpen>]
module ReversibleStackPanelBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a VStack widget.</summary>
        /// <param name="spacing">The spacing between each child.</param>
        /// <param name="reverseOrder">If true, the children will be reversed.</param>
        static member VStack(?spacing: float, ?reverseOrder: bool) =

            let mutable scalars =
                StackList.one(StackPanel.Orientation.WithValue(Orientation.Vertical))

            match spacing with
            | None -> ()
            | Some v ->
                let s = StackPanel.Spacing.WithValue(v)
                scalars <- StackList.add(&scalars, &s)

            match reverseOrder with
            | None -> ()
            | Some v ->
                let s = ReversibleStackPanel.ReverseOrder.WithValue(v)
                scalars <- StackList.add(&scalars, &s)

            let attr = Panel.Children
            CollectionBuilder<'msg, IFabReversibleStackPanel, IFabControl>(ReversibleStackPanel.WidgetKey, scalars, attr)

        /// <summary>Creates a HStack widget.</summary>
        /// <param name="spacing">The spacing between each child.</param>
        /// <param name="reverseOrder">If true, the children will be reversed.</param>
        static member HStack(?spacing: float, ?reverseOrder: bool) =

            let mutable scalars =
                StackList.one(StackPanel.Orientation.WithValue(Orientation.Horizontal))

            match spacing with
            | None -> ()
            | Some v ->
                let s = StackPanel.Spacing.WithValue(v)
                scalars <- StackList.add(&scalars, &s)

            match reverseOrder with
            | None -> ()
            | Some v ->
                let s = ReversibleStackPanel.ReverseOrder.WithValue(v)
                scalars <- StackList.add(&scalars, &s)

            let attr = Panel.Children
            CollectionBuilder<'msg, IFabReversibleStackPanel, IFabControl>(ReversibleStackPanel.WidgetKey, scalars, attr)

type ReversibleStackPanelModifiers =
    /// <summary>Link a ViewRef to access the direct ReversibleStackPanel control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabReversibleStackPanel>, value: ViewRef<ReversibleStackPanel>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
