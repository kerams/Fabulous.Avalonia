namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

module ComponentMaskedTextBox =
    let TextChanged =
        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "MaskedTextBox_TextChanged" MaskedTextBox.TextProperty

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
