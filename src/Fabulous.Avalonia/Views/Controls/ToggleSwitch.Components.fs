namespace Fabulous.Avalonia

open Avalonia.Animation
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

[<AutoOpen>]
module ComponentToggleSwitchBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ToggleSwitch widget.</summary>
        /// <param name="isChecked">Whether the ToggleSwitch is checked.</param>
        /// <param name="fn">Raised when the ToggleSwitch value changes.</param>
        static member ToggleSwitch(isChecked: bool, fn: bool -> unit) =
            let s1 = ToggleButton.IsThreeState.WithValue(false)
            let s2 = ComponentToggleButton.CheckedChanged.WithValue(ComponentValueEventData.create isChecked fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabToggleSwitch>(
                ToggleSwitch.WidgetKey,
                &bundle
            )

        /// <summary>Creates a ThreeStateToggleSwitch widget.</summary>
        /// <param name="isChecked">Whether the ToggleSwitch is checked.</param>
        /// <param name="fn">Raised when the ToggleSwitch value changes.</param>
        static member ThreeStateToggleSwitch(isChecked: bool option, fn: bool option -> unit) =
            let s1 = ToggleButton.IsThreeState.WithValue(true)
            let s2 = ComponentToggleButton.ThreeStateCheckedChanged.WithValue(
                ComponentValueEventData.createOptional (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn)
            )
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabToggleSwitch>(ToggleSwitch.WidgetKey, &bundle)
