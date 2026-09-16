namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Collections
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFaDashStyle =
    inherit IFabAnimatable

module DashStyle =
    let WidgetKey = Widgets.register<DashStyle>()

    let Dashes =
        Attributes.defineSimpleScalarWithEquality<float list> "DashStyle_Dashes" (fun _ newValue node ->
            let target = node.Target :?> AvaloniaObject

            if not newValue.HasValue then
                target.ClearValue(DashStyle.DashesProperty)
            else
                let points = newValue.Value
                let coll = AvaloniaList<float>()
                points |> List.iter coll.Add
                target.SetValue(DashStyle.DashesProperty, coll) |> ignore)

    let Offset = Attributes.defineAvaloniaPropertyFloat DashStyle.OffsetProperty

[<AutoOpen>]
module DashStyleBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DashStyle widget.</summary>
        /// <param name="dashes">The length of alternating dashes and gaps.</param>
        /// <param name="offset">How far in the dash sequence the stroke will start.</param>
        static member DashStyle(dashes: float list, offset: float) =
            let d = DashStyle.Dashes.WithValue(dashes)
            let o = DashStyle.Offset.WithValue(offset)
            let bundle = AttributesBundle(StackList.two(d, o), [||], [||])
            WidgetBuilder<'msg, IFaDashStyle>(DashStyle.WidgetKey, &bundle)

type DashStyleModifiers =

    /// <summary>Link a ViewRef to access the direct DashStyle control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFaDashStyle>, value: ViewRef<DashStyle>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
