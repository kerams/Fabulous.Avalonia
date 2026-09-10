namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

module MvuMaskedTextBox =
    let TextChanged =
        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "MaskedTextBox_TextChanged" MaskedTextBox.TextProperty

[<AutoOpen>]
module MvuMaskedTextBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a MaskedTextBox widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="mask">The mask to apply.</param>
        /// <param name="fn">Raised when the text changes.</param>
        static member inline MaskedTextBox(text: string, mask: string, fn: string -> 'msg) =
            let s1 = MaskedTextBox.Mask.WithValue(mask)
            let s2 = MvuMaskedTextBox.TextChanged.WithValue(ValueEventData.create text fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabMaskedTextBox>(
                MaskedTextBox.WidgetKey,
                &bundle
            )
