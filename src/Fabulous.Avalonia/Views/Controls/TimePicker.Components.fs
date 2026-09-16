namespace Fabulous.Avalonia

open System
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentTimePicker =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedTimeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<System.TimeSpan, System.TimeSpan>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectedTimeChangedInit: bool

    static member SelectedTimeChanged =
        if not ComponentTimePicker._SelectedTimeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTimePicker._SelectedTimeChangedInit then
                    ComponentTimePicker._SelectedTimeChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent
                            "TimePicker_SelectedTimeChanged"
                            TimePicker.SelectedTimeProperty
                            Nullable
                            Nullable.op_Explicit

                    ComponentTimePicker._SelectedTimeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTimePicker._SelectedTimeChanged

[<AutoOpen>]
module ComponentTimePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TimePicker widget.</summary>
        /// <param name="time">The initial time.</param>
        /// <param name="fn">Raised when the selected time changes.</param>
        static member TimePicker(time: TimeSpan, fn: TimeSpan -> unit) =
            let attr = ComponentTimePicker.SelectedTimeChanged.WithValue(ComponentValueEventData.create time fn)
            WidgetBuilder<'msg, IFabTimePicker>(TimePicker.WidgetKey, &attr)
