namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia


// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentCalendar =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<(System.DateTime option), (System.DateTime option)>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedDateChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DisplayDateChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.CalendarDateChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DisplayDateChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DisplayModeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.CalendarModeChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DisplayModeChangedInit: bool

    static member SelectedDateChanged =
        if not ComponentCalendar._SelectedDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendar._SelectedDateChangedInit then
                    ComponentCalendar._SelectedDateChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent
                            "Calendar_SelectedDateChanged"
                            Calendar.SelectedDateProperty
                            Option.toNullable
                            Option.ofNullable

                    ComponentCalendar._SelectedDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendar._SelectedDateChanged

    static member DisplayDateChanged =
        if not ComponentCalendar._DisplayDateChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendar._DisplayDateChangedInit then
                    ComponentCalendar._DisplayDateChanged <-
                        Attributes.Component.defineEvent "Calendar_DisplayDateChanged" (fun target -> (target :?> Calendar).DisplayDateChanged)

                    ComponentCalendar._DisplayDateChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendar._DisplayDateChanged

    static member DisplayModeChanged =
        if not ComponentCalendar._DisplayModeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentCalendar._DisplayModeChangedInit then
                    ComponentCalendar._DisplayModeChanged <-
                        Attributes.Component.defineEvent "Calendar_DisplayModeChanged" (fun target -> (target :?> Calendar).DisplayModeChanged)

                    ComponentCalendar._DisplayModeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentCalendar._DisplayModeChanged

[<AutoOpen>]
module ComponentCalendarBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Calendar widget.</summary>
        /// <param name="date">The date to display.</param>
        /// <param name="fn">Raised when the date changes.</param>
        static member Calendar(date: DateTime option, fn: DateTime option -> unit) =
            let s1 = Calendar.SelectionMode.WithValue(CalendarSelectionMode.SingleDate)
            let s2 = ComponentCalendar.SelectedDateChanged.WithValue(ComponentValueEventData.create date fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCalendar>(
                Calendar.WidgetKey,
                &bundle
            )

        /// <summary>Creates a Calendar widget.</summary>
        /// <param name="date">The date to display.</param>
        /// <param name="fn">Raised when the date changes.</param>
        /// <param name="mode">The selection mode.</param>
        static member Calendar(date: DateTime option, fn: DateTime option -> unit, mode: CalendarSelectionMode) =
            let s1 = Calendar.SelectionMode.WithValue(mode)
            let s2 = ComponentCalendar.SelectedDateChanged.WithValue(ComponentValueEventData.create date fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabCalendar>(
                Calendar.WidgetKey,
                &bundle
            )

type ComponentCalendarModifiers =
    /// <summary>Listens to the Calendar DisplayDateChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DisplayDateChanged event is fired.</param>
    [<Extension>]
    static member inline onDisplayDateChanged(this: WidgetBuilder<'msg, #IFabCalendar>, fn: CalendarDateChangedEventArgs -> unit) =
        this.AddScalar(ComponentCalendar.DisplayDateChanged.WithValue(fn))

    /// <summary>Listens to the Calendar DisplayModeChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DisplayModeChanged event is fired.</param>
    [<Extension>]
    static member inline onDisplayModeChanged(this: WidgetBuilder<'msg, #IFabCalendar>, fn: CalendarModeChangedEventArgs -> unit) =
        this.AddScalar(ComponentCalendar.DisplayModeChanged.WithValue(fn))
