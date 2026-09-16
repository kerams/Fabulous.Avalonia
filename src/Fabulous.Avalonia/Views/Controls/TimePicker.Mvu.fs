namespace Fabulous.Avalonia

open System
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTimePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedTimeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<System.TimeSpan, System.TimeSpan>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedTimeChangedInit: bool

    static member SelectedTimeChanged =
        if not MvuTimePicker._SelectedTimeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTimePicker._SelectedTimeChangedInit then
                    MvuTimePicker._SelectedTimeChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent "TimePicker_SelectedTimeChanged" TimePicker.SelectedTimeProperty Nullable Nullable.op_Explicit

                    MvuTimePicker._SelectedTimeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTimePicker._SelectedTimeChanged

[<AutoOpen>]
module MvuTimePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TimePicker widget.</summary>
        /// <param name="time">The initial time.</param>
        /// <param name="fn">Raised when the selected time changes.</param>
        static member TimePicker(time: TimeSpan, fn: TimeSpan -> 'msg) =
            let attr = MvuTimePicker.SelectedTimeChanged.WithValue(ValueEventData.create time fn)
            WidgetBuilder<'msg, IFabTimePicker>(TimePicker.WidgetKey, &attr)
