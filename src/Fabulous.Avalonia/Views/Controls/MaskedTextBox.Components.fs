namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentMaskedTextBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<string, string>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChangedInit: bool

    static member TextChanged =
        if not ComponentMaskedTextBox._TextChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMaskedTextBox._TextChangedInit then
                    ComponentMaskedTextBox._TextChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "MaskedTextBox_TextChanged" MaskedTextBox.TextProperty

                    ComponentMaskedTextBox._TextChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMaskedTextBox._TextChanged

[<AutoOpen>]
module ComponentMaskedTextBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a MaskedTextBox widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="mask">The mask to apply.</param>
        /// <param name="fn">Raised when the text changes.</param>
        static member inline MaskedTextBox(text: string, mask: string, fn: string -> unit) =
            let s1 = MaskedTextBox.Mask.WithValue(mask)
            let s2 = ComponentMaskedTextBox.TextChanged.WithValue(ComponentValueEventData.create text fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabMaskedTextBox>(
                MaskedTextBox.WidgetKey,
                &bundle
            )
