namespace Fabulous.Avalonia

open System
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentDatePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<System.DateTimeOffset, System.DateTimeOffset>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChangedInit: bool

    static member SelectedDateChanged =
        if not ComponentDatePicker._SelectedDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentDatePicker._SelectedDateChangedInit then
                    ComponentDatePicker._SelectedDateChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent
                            "DatePicker_SelectedDateChanged"
                            DatePicker.SelectedDateProperty
                            Nullable
                            Nullable.op_Explicit

                    ComponentDatePicker._SelectedDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentDatePicker._SelectedDateChanged

[<AutoOpen>]
module ComponentDatePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DatePicker widget.</summary>
        /// <param name="date">The initial date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member DatePicker(date: DateTimeOffset, fn: DateTimeOffset -> unit) =
            let attr = ComponentDatePicker.SelectedDateChanged.WithValue(ComponentValueEventData.create date fn)
            WidgetBuilder<'msg, IFabDatePicker>(DatePicker.WidgetKey, &attr)
