namespace Fabulous.Avalonia


open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module MvuSliderBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Slider widget.</summary>
        /// <param name="value">The initial value of the slider.</param>
        /// <param name="fn">Raised when the slider value changes.</param>
        static member Slider(value: float, fn: float -> 'msg) =
            let attr = MvuRangeBase.ValueChanged.WithValue(ValueEventData.create value fn)
            WidgetBuilder<'msg, IFabSlider>(Slider.WidgetKey, &attr)

        /// <summary>Creates a Slider widget.</summary>
        /// <param name="min">The minimum value of the slider.</param>
        /// <param name="max">The maximum value of the slider.</param>
        /// <param name="value">The initial value of the slider.</param>
        /// <param name="fn">Raised when the slider value changes.</param>
        static member inline Slider(min: float, max: float, value: float, fn: float -> 'msg) =
            let s1 = RangeBase.MinimumMaximum.WithValue(struct (min, max))
            let s2 = MvuRangeBase.ValueChanged.WithValue(ValueEventData.create value fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabSlider>(Slider.WidgetKey, &bundle)
