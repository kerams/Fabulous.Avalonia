namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

[<AutoOpen>]
module MvuButtonSpinnerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ButtonSpinner widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the ButtonSpinner is clicked.</param>
        static member ButtonSpinner(text: string, fn: SpinEventArgs -> 'msg) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = MvuSpinner.Spin.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabButtonSpinner>(ButtonSpinner.WidgetKey, &bundle)
