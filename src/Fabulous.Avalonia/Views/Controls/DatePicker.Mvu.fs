namespace Fabulous.Avalonia

open System
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuDatePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<System.DateTimeOffset, System.DateTimeOffset>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChangedInit: bool

    static member SelectedDateChanged =
        if not MvuDatePicker._SelectedDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuDatePicker._SelectedDateChangedInit then
                    MvuDatePicker._SelectedDateChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent "DatePicker_SelectedDateChanged" DatePicker.SelectedDateProperty Nullable Nullable.op_Explicit

                    MvuDatePicker._SelectedDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuDatePicker._SelectedDateChanged

[<AutoOpen>]
module MvuDatePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DatePicker widget.</summary>
        /// <param name="date">The initial date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member DatePicker(date: DateTimeOffset, fn: DateTimeOffset -> 'msg) =
            let attr = MvuDatePicker.SelectedDateChanged.WithValue(ValueEventData.create date fn)
            WidgetBuilder<'msg, IFabDatePicker>(DatePicker.WidgetKey, &attr)
