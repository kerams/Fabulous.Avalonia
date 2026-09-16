namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentScrollBar =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Scroll: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.Primitives.ScrollEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScrollInit: bool

    static member Scroll =
        if not ComponentScrollBar._ScrollInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentScrollBar._ScrollInit then
                    ComponentScrollBar._Scroll <-
                        Attributes.Component.defineEvent "ScrollBar_Scroll" (fun target -> (target :?> ScrollBar).Scroll)

                    ComponentScrollBar._ScrollInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentScrollBar._Scroll

[<AutoOpen>]
module ComponentScrollBarBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ScrollBar widget.</summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="value">Current value.</param>
        /// <param name="fn">Raised when the value changes.</param>
        static member inline ScrollBar(min: float, max: float, value: float, fn: float -> unit) =
            let s1 = RangeBase.MinimumMaximum.WithValue(struct (min, max))
            let s2 = ComponentRangeBase.ValueChanged.WithValue(ComponentValueEventData.create value fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabScrollBar>(ScrollBar.WidgetKey, &bundle)

type ComponentScrollBarModifiers =
    /// <summary>Listens to the ScrollBar Scroll event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Scroll value changes.</param>
    [<Extension>]
    static member inline onScroll(this: WidgetBuilder<'msg, #IFabScrollBar>, fn: ScrollEventArgs -> unit) =
        this.AddScalar(ComponentScrollBar.Scroll.WithValue(fn))
