namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuCalendarDatePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<(System.DateTime option), (System.DateTime option)>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DateValidationError: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.CalendarDatePickerDateValidationErrorEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DateValidationErrorInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarOpenedInit: bool

    static member SelectedDateChanged =
        if not MvuCalendarDatePicker._SelectedDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuCalendarDatePicker._SelectedDateChangedInit then
                    MvuCalendarDatePicker._SelectedDateChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent
                            "CalendarDatePicker_SelectedDateChanged"
                            CalendarDatePicker.SelectedDateProperty
                            Option.toNullable
                            Option.ofNullable

                    MvuCalendarDatePicker._SelectedDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuCalendarDatePicker._SelectedDateChanged

    static member DateValidationError =
        if not MvuCalendarDatePicker._DateValidationErrorInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuCalendarDatePicker._DateValidationErrorInit then
                    MvuCalendarDatePicker._DateValidationError <-
                        Attributes.Mvu.defineEvent "CalendarDatePicker_DateValidationError" (fun target -> (target :?> CalendarDatePicker).DateValidationError)

                    MvuCalendarDatePicker._DateValidationErrorInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuCalendarDatePicker._DateValidationError

    static member CalendarClosed =
        if not MvuCalendarDatePicker._CalendarClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuCalendarDatePicker._CalendarClosedInit then
                    MvuCalendarDatePicker._CalendarClosed <-
                        Attributes.Mvu.defineEventNoArg "CalendarDatePicker_CalendarClosed" (fun target -> (target :?> CalendarDatePicker).CalendarClosed)

                    MvuCalendarDatePicker._CalendarClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuCalendarDatePicker._CalendarClosed

    static member CalendarOpened =
        if not MvuCalendarDatePicker._CalendarOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuCalendarDatePicker._CalendarOpenedInit then
                    MvuCalendarDatePicker._CalendarOpened <-
                        Attributes.Mvu.defineEventNoArg "CalendarDatePicker_CalendarOpened" (fun target -> (target :?> CalendarDatePicker).CalendarOpened)

                    MvuCalendarDatePicker._CalendarOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuCalendarDatePicker._CalendarOpened

[<AutoOpen>]
module MvuCalendarDatePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a CalendarDatePicker widget.</summary>
        /// <param name="date">The selected date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member CalendarDatePicker(date: DateTime option, fn: DateTime option -> 'msg) =
            let attr = MvuCalendarDatePicker.SelectedDateChanged.WithValue(ValueEventData.create date fn)
            WidgetBuilder<'msg, IFabCalendarDatePicker>(CalendarDatePicker.WidgetKey, &attr)

type MvuCalendarDatePickerModifiers =
    /// <summary>Listens to the CalendarDatePicker DateValidationError event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker detects a format error.</param>
    [<Extension>]
    static member inline onDateValidationError(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: CalendarDatePickerDateValidationErrorEventArgs -> 'msg) =
        this.AddScalar(MvuCalendarDatePicker.DateValidationError.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker closes its calendar.</param>
    [<Extension>]
    static member inline onCalendarClosed(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: 'msg) =
        this.AddScalar(MvuCalendarDatePicker.CalendarClosed.WithValue(MsgValue fn))

    /// <summary>Listens to the CalendarDatePicker CalendarOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker opens its calendar.</param>
    [<Extension>]
    static member inline onCalendarOpened(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: 'msg) =
        this.AddScalar(MvuCalendarDatePicker.CalendarOpened.WithValue(MsgValue fn))
