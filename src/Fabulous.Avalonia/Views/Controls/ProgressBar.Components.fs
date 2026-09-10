namespace Fabulous.Avalonia

open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module ComponentProgressBarBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ProgressBar widget.</summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="value">Current value.</param>
        /// <param name="fn">Raised when the value changes.</param>
        static member ProgressBar(min: float, max: float, value: float, fn: float -> unit) =
            let s1 = RangeBase.MinimumMaximum.WithValue(struct (min, max))
            let s2 = ComponentRangeBase.ValueChanged.WithValue(ComponentValueEventData.create value fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabProgressBar>(ProgressBar.WidgetKey, &bundle)
