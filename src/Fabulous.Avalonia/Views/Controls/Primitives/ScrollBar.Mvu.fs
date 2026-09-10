namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module MvuScrollBar =
    let Scroll =
        Attributes.Mvu.defineEvent "ScrollBar_Scroll" (fun target -> (target :?> ScrollBar).Scroll)

[<AutoOpen>]
module MvuScrollBarBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ScrollBar widget.</summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="value">Current value.</param>
        /// <param name="fn">Raised when the value changes.</param>
        static member inline ScrollBar(min: float, max: float, value: float, fn: float -> 'msg) =
            let s1 = RangeBase.MinimumMaximum.WithValue(struct (min, max))
            let s2 = MvuRangeBase.ValueChanged.WithValue(ValueEventData.create value fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabScrollBar>(ScrollBar.WidgetKey, &bundle)

type MvuScrollBarModifiers =
    /// <summary>Listens to the ScrollBar Scroll event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Scroll value changes.</param>
    [<Extension>]
    static member inline onScroll(this: WidgetBuilder<'msg, #IFabScrollBar>, fn: ScrollEventArgs -> 'msg) =
        this.AddScalar(MvuScrollBar.Scroll.WithValue(fn))
