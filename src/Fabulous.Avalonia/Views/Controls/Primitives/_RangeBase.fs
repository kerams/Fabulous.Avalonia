namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous

type IFabRangeBase =
    inherit IFabTemplatedControl

module RangeBaseUpdaters =
    let updateSliderMinMax _ (newValue: ScalarValue<struct (float * float)>) (node: IViewNode) =
        let slider = node.Target :?> RangeBase

        if not newValue.HasValue then
            slider.ClearValue(RangeBase.MinimumProperty)
            slider.ClearValue(RangeBase.MaximumProperty)
        else
            let struct (min, max) = newValue.Value
            let currMax = slider.GetValue(RangeBase.MaximumProperty)

            if min > currMax then
                slider.SetValue(RangeBase.MaximumProperty, max) |> ignore
                slider.SetValue(RangeBase.MinimumProperty, min) |> ignore
            else
                slider.SetValue(RangeBase.MinimumProperty, min) |> ignore
                slider.SetValue(RangeBase.MaximumProperty, max) |> ignore

module RangeBase =
    let MinimumMaximum =
        Attributes.defineSimpleScalarWithEquality<struct (float * float)> "RangeBase_MinimumMaximum" RangeBaseUpdaters.updateSliderMinMax

    let Minimum =
        Attributes.defineAvaloniaPropertyFloat RangeBase.MinimumProperty

    let Maximum =
        Attributes.defineAvaloniaPropertyFloat RangeBase.MaximumProperty

    let SmallChange =
        Attributes.defineAvaloniaPropertyFloat RangeBase.SmallChangeProperty

    let LargeChange =
        Attributes.defineAvaloniaPropertyFloat RangeBase.LargeChangeProperty

type RangeBaserModifiers =
    /// <summary>Sets the SmallChange property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The SmallChange value.</param>
    [<Extension>]
    static member inline smallChange(this: WidgetBuilder<'msg, #IFabRangeBase>, value: float) =
        this.AddScalar(RangeBase.SmallChange.WithValue(value))

    /// <summary>Sets the LargeChange property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The LargeChange value.</param>
    [<Extension>]
    static member inline largeChange(this: WidgetBuilder<'msg, #IFabRangeBase>, value: float) =
        this.AddScalar(RangeBase.LargeChange.WithValue(value))
