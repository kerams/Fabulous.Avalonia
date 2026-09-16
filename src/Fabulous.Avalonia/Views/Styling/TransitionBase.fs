namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Animation
open Avalonia.Animation.Easings
open Fabulous
open Fabulous.StackAllocatedCollections

// DoubleTransition lives apart from the other transitions in Transition.fs: F# initializes all top-level values of
// a file together, so using any one transition would otherwise register (and root) all of them under NativeAOT.
type IFabTransition =
    inherit IFabElement

module TransitionBase =
    let Duration =
        Attributes.defineAvaloniaPropertyWithEquality TransitionBase.DurationProperty

    let Delay =
        Attributes.defineAvaloniaPropertyWithEquality TransitionBase.DelayProperty

    let Easing =
        Attributes.defineAvaloniaPropertyWithEquality TransitionBase.EasingProperty

    let Property =
        Attributes.defineAvaloniaPropertyWithEquality TransitionBase.PropertyProperty

type TransitionBaseModifiers =
    /// <summary>Sets the Delay property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Delay value.</param>
    [<Extension>]
    static member inline delay(this: WidgetBuilder<'msg, #IFabTransition>, value: TimeSpan) =
        this.AddScalar(TransitionBase.Delay.WithValue(value))

    /// <summary>Sets the Easing property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Easing value.</param>
    [<Extension>]
    static member inline easing(this: WidgetBuilder<'msg, #IFabTransition>, value: Easing) =
        this.AddScalar(TransitionBase.Easing.WithValue(value))

    /// <summary>Link a ViewRef to access the direct DoubleTransition control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTransition>, value: ViewRef<#TransitionBase>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

type IFabDoubleTransition =
    inherit IFabTransition

module DoubleTransition =
    let WidgetKey = Widgets.register<DoubleTransition>()

[<AutoOpen>]
module DoubleTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a DoubleTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member DoubleTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabDoubleTransition>(DoubleTransition.WidgetKey, &propAttr, &durationAttr)

type TransitionBaseCollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'marker :> IFabAnimatable and 'itemType :> IFabTransition>
        (_: AttributeCollectionBuilder<'msg, 'marker, IFabTransition>, x: WidgetBuilder<'msg, 'itemType>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'marker :> IFabAnimatable and 'itemType :> IFabTransition>
        (_: AttributeCollectionBuilder<'msg, 'marker, IFabTransition>, x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

type TransitionCollectionModifiers =
    /// <summary>Sets the Transitions property.</summary>
    /// <param name="this">Current widget.</param>
    [<Extension>]
    static member inline transition(this: WidgetBuilder<'msg, #IFabAnimatable>) =
        let attr = Animatable.Transitions
        AttributeCollectionBuilder<'msg, #IFabAnimatable, #IFabTransition>(&this, &attr)

    /// <summary>Sets the Transition property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Transition value.</param>
    [<Extension>]
    static member inline transition(this: WidgetBuilder<'msg, #IFabAnimatable>, value: WidgetBuilder<'msg, #IFabTransition>) =
        TransitionCollectionModifiers.transition(this) { value }
