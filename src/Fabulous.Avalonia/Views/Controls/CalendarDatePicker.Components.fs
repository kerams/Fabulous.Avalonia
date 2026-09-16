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
type ComponentCalendarDatePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<(System.DateTime option), (System.DateTime option)>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DateValidationError: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.CalendarDatePickerDateValidationErrorEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DateValidationErrorInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CalendarOpenedInit: bool

    static member SelectedDateChanged =
        if not ComponentCalendarDatePicker._SelectedDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendarDatePicker._SelectedDateChangedInit then
                    ComponentCalendarDatePicker._SelectedDateChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent
                            "CalendarDatePicker_SelectedDateChanged"
                            CalendarDatePicker.SelectedDateProperty
                            Option.toNullable
                            Option.ofNullable

                    ComponentCalendarDatePicker._SelectedDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendarDatePicker._SelectedDateChanged

    static member DateValidationError =
        if not ComponentCalendarDatePicker._DateValidationErrorInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendarDatePicker._DateValidationErrorInit then
                    ComponentCalendarDatePicker._DateValidationError <-
                        Attributes.Component.defineEvent "CalendarDatePicker_DateValidationError" (fun target -> (target :?> CalendarDatePicker).DateValidationError)

                    ComponentCalendarDatePicker._DateValidationErrorInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendarDatePicker._DateValidationError

    static member CalendarClosed =
        if not ComponentCalendarDatePicker._CalendarClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendarDatePicker._CalendarClosedInit then
                    ComponentCalendarDatePicker._CalendarClosed <-
                        Attributes.Component.defineEventNoArg "CalendarDatePicker_CalendarClosed" (fun target -> (target :?> CalendarDatePicker).CalendarClosed)

                    ComponentCalendarDatePicker._CalendarClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendarDatePicker._CalendarClosed

    static member CalendarOpened =
        if not ComponentCalendarDatePicker._CalendarOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendarDatePicker._CalendarOpenedInit then
                    ComponentCalendarDatePicker._CalendarOpened <-
                        Attributes.Component.defineEventNoArg "CalendarDatePicker_CalendarOpened" (fun target -> (target :?> CalendarDatePicker).CalendarOpened)

                    ComponentCalendarDatePicker._CalendarOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendarDatePicker._CalendarOpened

[<AutoOpen>]
module ComponentCalendarDatePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a CalendarDatePicker widget.</summary>
        /// <param name="date">The selected date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member CalendarDatePicker(date: DateTime option, fn: DateTime option -> unit) =
            let attr = ComponentCalendarDatePicker.SelectedDateChanged.WithValue(ComponentValueEventData.create date fn)
            WidgetBuilder<'msg, IFabCalendarDatePicker>(CalendarDatePicker.WidgetKey, &attr)

type ComponentCalendarDatePickerModifiers =
    /// <summary>Listens to the CalendarDatePicker DateValidationError event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker detects a format error.</param>
    [<Extension>]
    static member inline onDateValidationError(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: CalendarDatePickerDateValidationErrorEventArgs -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.DateValidationError.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker closes its calendar.</param>
    [<Extension>]
    static member inline onCalendarClosed(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.CalendarClosed.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker opens its calendar.</param>
    [<Extension>]
    static member inline onCalendarOpened(this: WidgetBuilder<'msg, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.CalendarOpened.WithValue(fn))
