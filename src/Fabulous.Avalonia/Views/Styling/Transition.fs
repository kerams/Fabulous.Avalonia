namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Animation
open Avalonia.Animation.Easings
open Fabulous
open Fabulous.StackAllocatedCollections

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

type IFabBoxShadowsTransition =
    inherit IFabTransition

module BoxShadowsTransition =

    let WidgetKey = Widgets.register<BoxShadowsTransition>()

[<AutoOpen>]
module BoxShadowsTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a BoxShadowsTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member BoxShadowsTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabBoxShadowsTransition>(BoxShadowsTransition.WidgetKey, &propAttr, &durationAttr)

type IFabBrushTransition =
    inherit IFabTransition

module BrushTransition =

    let WidgetKey = Widgets.register<BrushTransition>()

[<AutoOpen>]
module BrushTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a BrushTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member BrushTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabBrushTransition>(BrushTransition.WidgetKey, &propAttr, &durationAttr)

type IFabColorTransition =
    inherit IFabTransition

module ColorTransition =
    let WidgetKey = Widgets.register<ColorTransition>()

[<AutoOpen>]
module ColorTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a ColorTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member ColorTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabColorTransition>(ColorTransition.WidgetKey, &propAttr, &durationAttr)

type IFabCornerRadiusTransition =
    inherit IFabTransition

module CornerRadiusTransition =

    let WidgetKey = Widgets.register<CornerRadiusTransition>()

[<AutoOpen>]
module CornerRadiusTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a CornerRadiusTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member CornerRadiusTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabCornerRadiusTransition>(CornerRadiusTransition.WidgetKey, &propAttr, &durationAttr)

type IFabFloatTransition =
    inherit IFabTransition

module FloatTransition =
    let WidgetKey = Widgets.register<FloatTransition>()

[<AutoOpen>]
module FloatTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a FloatTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member FloatTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabFloatTransition>(FloatTransition.WidgetKey, &propAttr, &durationAttr)

type IFabIntegerTransition =
    inherit IFabTransition

module IntegerTransition =
    let WidgetKey = Widgets.register<IntegerTransition>()

[<AutoOpen>]
module IntegerTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a IntegerTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member IntegerTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabIntegerTransition>(IntegerTransition.WidgetKey, &propAttr, &durationAttr)

type IFabPointTransition =
    inherit IFabTransition

module PointTransition =

    let WidgetKey = Widgets.register<PointTransition>()

[<AutoOpen>]
module PointTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a PointTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member PointTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabPointTransition>(PointTransition.WidgetKey, &propAttr, &durationAttr)

type IFabSizeTransition =
    inherit IFabTransition

module SizeTransition =

    let WidgetKey = Widgets.register<SizeTransition>()

[<AutoOpen>]
module SizeTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a SizeTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member SizeTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabSizeTransition>(SizeTransition.WidgetKey, &propAttr, &durationAttr)

type IFabThicknessTransition =
    inherit IFabTransition

module ThicknessTransition =

    let WidgetKey = Widgets.register<ThicknessTransition>()

[<AutoOpen>]
module ThicknessTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a ThicknessTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member ThicknessTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabThicknessTransition>(ThicknessTransition.WidgetKey, &propAttr, &durationAttr)

type IFabTransformOperationsTransition =
    inherit IFabTransition

module TransformOperationsTransition =

    let WidgetKey = Widgets.register<TransformOperationsTransition>()

[<AutoOpen>]
module TransformOperationsTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a TransformOperationsTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member TransformOperationsTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabTransformOperationsTransition>(TransformOperationsTransition.WidgetKey, &propAttr, &durationAttr)

type IFabVectorTransition =
    inherit IFabTransition

module VectorTransition =

    let WidgetKey = Widgets.register<VectorTransition>()

[<AutoOpen>]
module VectorTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a VectorTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member VectorTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabVectorTransition>(VectorTransition.WidgetKey, &propAttr, &durationAttr)

type IFabBoolTransition =
    inherit IFabTransition

module BoolTransition =
    let WidgetKey = Widgets.register<BoolTransition>()

[<AutoOpen>]
module BoolTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a BoolTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member BoolTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabBoolTransition>(BoolTransition.WidgetKey, &propAttr, &durationAttr)

type IFabEffectTransition =
    inherit IFabTransition

module EffectTransition =
    let WidgetKey = Widgets.register<EffectTransition>()

[<AutoOpen>]
module EffectTransitionBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a EffectTransition widget.</summary>
        /// <param name="property">The property to animate.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member EffectTransition(property: AvaloniaProperty, duration: TimeSpan) =
            let propAttr = TransitionBase.Property.WithValue(property)
            let durationAttr = TransitionBase.Duration.WithValue(duration)
            WidgetBuilder<'msg, IFabEffectTransition>(EffectTransition.WidgetKey, &propAttr, &durationAttr)

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
