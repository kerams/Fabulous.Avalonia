namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList


// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuNumericUpDown =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<(System.Decimal option), (System.Decimal option)>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChangedInit: bool

    static member ValueChanged =
        if not MvuNumericUpDown._ValueChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuNumericUpDown._ValueChangedInit then
                    MvuNumericUpDown._ValueChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent "NumericUpDown_ValueChanged" NumericUpDown.ValueProperty Option.toNullable Option.ofNullable

                    MvuNumericUpDown._ValueChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuNumericUpDown._ValueChanged

[<AutoOpen>]
module MvuNumericUpDownBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a NumericUpDown widget.</summary>
        /// <param name="value">The value of the NumericUpDown.</param>
        /// <param name="fn">Raised when the NumericUpDown value changes.</param>
        static member NumericUpDown(value: float option, fn: float option -> 'msg) =
            let s =
                MvuNumericUpDown.ValueChanged.WithValue(
                    let value =
                        match value with
                        | Some v -> Some(decimal v)
                        | None -> None

                    ValueEventData.create value (Option.map float >> fn)
                )
            WidgetBuilder<'msg, IFabNumericUpDown>(NumericUpDown.WidgetKey, &s)

        /// <summary>Creates a NumericUpDown widget.</summary>
        /// <param name="min">The minimum value of the NumericUpDown.</param>
        /// <param name="max">The maximum value of the NumericUpDown.</param>
        /// <param name="value">The value of the NumericUpDown.</param>
        /// <param name="fn">Raised when the NumericUpDown value changes.</param>
        static member NumericUpDown(min: float, max: float, value: float option, fn: float option -> 'msg) =
            let s1 = NumericUpDown.MinimumMaximum.WithValue(struct (decimal min, decimal max))
            let s2 =
                MvuNumericUpDown.ValueChanged.WithValue(
                    let value =
                        match value with
                        | Some v -> Some(decimal v)
                        | None -> None

                    ValueEventData.create value (Option.map float >> fn)
                )
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabNumericUpDown>(NumericUpDown.WidgetKey, &bundle)
