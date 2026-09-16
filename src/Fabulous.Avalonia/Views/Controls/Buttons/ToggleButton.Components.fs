namespace Fabulous.Avalonia

open System
open Avalonia.Controls.Primitives
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentToggleButton =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CheckedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CheckedChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ThreeStateCheckedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<System.Nullable<bool>, System.Nullable<bool>>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ThreeStateCheckedChangedInit: bool

    static member CheckedChanged =
        if not ComponentToggleButton._CheckedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentToggleButton._CheckedChangedInit then
                    ComponentToggleButton._CheckedChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent "ToggleButton_IsCheckedChanged" ToggleButton.IsCheckedProperty Nullable Nullable.op_Explicit

                    ComponentToggleButton._CheckedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentToggleButton._CheckedChanged

    static member ThreeStateCheckedChanged =
        if not ComponentToggleButton._ThreeStateCheckedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentToggleButton._ThreeStateCheckedChangedInit then
                    ComponentToggleButton._ThreeStateCheckedChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "ToggleButton_CheckedChanged" ToggleButton.IsCheckedProperty

                    ComponentToggleButton._ThreeStateCheckedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentToggleButton._ThreeStateCheckedChanged

[<AutoOpen>]
module ComponentToggleButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ToggleButton widget.</summary>
        /// <param name="text">The text of the ToggleButton.</param>
        /// <param name="isChecked">Whether the ToggleButton is checked.</param>
        /// <param name="fn">Raised when the ToggleButton is clicked.</param>
        static member ToggleButton(text: string, isChecked: bool, fn: bool -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ComponentToggleButton.CheckedChanged.WithValue(ComponentValueEventData.create isChecked fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabToggleButton>(ToggleButton.WidgetKey, &bundle)

        /// <summary>Creates a ThreeStateToggleButton widget.</summary>
        /// <param name="text">The text of the ThreeStateToggleButton.</param>
        /// <param name="isChecked">Whether the ThreeStateToggleButton is checked.</param>
        /// <param name="fn">Raised when the ThreeStateToggleButton is clicked.</param>
        static member ThreeStateToggleButton(text: string, isChecked: bool option, fn: bool option -> unit) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = ToggleButton.IsThreeState.WithValue(true)
            let s3 = ComponentToggleButton.ThreeStateCheckedChanged.WithValue(
                        ComponentValueEventData.createOptional (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn)
                     )
            let bundle = AttributesBundle(StackList.three(s1, s2, s3), [||], [||])
            WidgetBuilder<'msg, IFabToggleButton>(ToggleButton.WidgetKey, &bundle)

        /// <summary>Creates a ToggleButton widget.</summary>
        /// <param name="isChecked">Whether the ToggleButton is checked.</param>
        /// <param name="fn">Raised when the ToggleButton is clicked.</param>
        /// <param name="content">The content of the ToggleButton.</param>
        static member ToggleButton(isChecked: bool, fn: bool -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(
                StackList.one(ComponentToggleButton.CheckedChanged.WithValue(ComponentValueEventData.create isChecked fn)),
                [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                [||]
            )
            WidgetBuilder<'msg, IFabToggleButton>(ToggleButton.WidgetKey, &bundle)

        /// <summary>Creates a ThreeStateToggleButton widget.</summary>
        /// <param name="isChecked">Whether the ThreeStateToggleButton is checked.</param>
        /// <param name="fn">Raised when the ThreeStateToggleButton is clicked.</param>
        /// <param name="content">The content of the ThreeStateToggleButton.</param>
        static member ThreeStateToggleButton(isChecked: bool option, fn: bool option -> unit, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(
                StackList.two(
                    ComponentToggleButton.ThreeStateCheckedChanged.WithValue(
                        ComponentValueEventData.createOptional (ThreeState.fromOption(isChecked)) (ThreeState.toOption >> fn)
                    ),
                    ToggleButton.IsThreeState.WithValue(true)
                ),
                [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                [||]
            )
            WidgetBuilder<'msg, IFabToggleButton>(ToggleButton.WidgetKey, &bundle)
