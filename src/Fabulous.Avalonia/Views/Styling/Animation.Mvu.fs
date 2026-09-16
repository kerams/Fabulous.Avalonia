namespace Fabulous.Avalonia

open System
open Avalonia.Animation
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuAnimation =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Children: Fabulous.WidgetCollectionAttributeDefinitions.WidgetCollectionAttributeDefinition

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ChildrenInit: bool

    static member Children =
        if not MvuAnimation._ChildrenInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAnimation._ChildrenInit then
                    MvuAnimation._Children <-
                        Attributes.defineAvaloniaListWidgetCollection "Animation_KeyFramesProperty" (fun target -> (target :?> Animation).Children)

                    MvuAnimation._ChildrenInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAnimation._Children

[<AutoOpen>]
module MvuAnimationBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates an Animation widget with the specified duration and keyframes.</summary>
        /// <param name="duration">The main Window of the Application.</param>
        static member Animation(duration: TimeSpan) =
            let attr = MvuAnimation.Children
            let scalar = Animation.Duration.WithValue(duration)
            CollectionBuilder<'msg, IFabAnimation, IFabKeyFrame>(Animation.WidgetKey, attr, scalar)

        /// <summary>Creates an Animation widget with keyframes.</summary>
        static member Animation() =
            let attr = MvuAnimation.Children
            CollectionBuilder<'msg, IFabAnimation, IFabKeyFrame>(Animation.WidgetKey, attr)

[<AutoOpen>]
module MvuAnimationAttachedBuilders =
    type Fabulous.Avalonia.View with

        /// <summary> Creates a Animation widget with the specified duration and keyframes.</summary>
        /// <param name="keyFrame">The keyframe to add to the animation.</param>
        /// <param name="duration">The duration of the animation.</param>
        static member inline Animation(keyFrame: WidgetBuilder<'msg, IFabKeyFrame>, duration: TimeSpan) =
            CollectionBuilder<'msg, IFabAnimation, IFabKeyFrame>(Animation.WidgetKey, MvuAnimation.Children, Animation.Duration.WithValue(duration)) {
                keyFrame
            }
