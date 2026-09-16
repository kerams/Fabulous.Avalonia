namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentNumericUpDown =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<(System.Decimal option), (System.Decimal option)>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChangedInit: bool

    static member ValueChanged =
        if not ComponentNumericUpDown._ValueChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNumericUpDown._ValueChangedInit then
                    ComponentNumericUpDown._ValueChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent "NumericUpDown_ValueChanged" NumericUpDown.ValueProperty Option.toNullable Option.ofNullable

                    ComponentNumericUpDown._ValueChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNumericUpDown._ValueChanged

[<AutoOpen>]
module ComponentNumericUpDownBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a NumericUpDown widget.</summary>
        /// <param name="value">The value of the NumericUpDown.</param>
        /// <param name="fn">Raised when the NumericUpDown value changes.</param>
        static member NumericUpDown(value: float option, fn: float option -> unit) =
            let s =
                ComponentNumericUpDown.ValueChanged.WithValue(
                    let value =
                        match value with
                        | Some v -> Some(decimal v)
                        | None -> None

                    ComponentValueEventData.create value (Option.map float >> fn)
                )
            WidgetBuilder<'msg, IFabNumericUpDown>(NumericUpDown.WidgetKey, &s)

        /// <summary>Creates a NumericUpDown widget.</summary>
        /// <param name="min">The minimum value of the NumericUpDown.</param>
        /// <param name="max">The maximum value of the NumericUpDown.</param>
        /// <param name="value">The value of the NumericUpDown.</param>
        /// <param name="fn">Raised when the NumericUpDown value changes.</param>
        static member NumericUpDown(min: float, max: float, value: float option, fn: float option -> unit) =
            let s1 = NumericUpDown.MinimumMaximum.WithValue(struct (decimal min, decimal max))
            let s2 =
                ComponentNumericUpDown.ValueChanged.WithValue(
                    let value =
                        match value with
                        | Some v -> Some(decimal v)
                        | None -> None

                    ComponentValueEventData.create value (Option.map float >> fn)
                )
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabNumericUpDown>(NumericUpDown.WidgetKey, &bundle)
