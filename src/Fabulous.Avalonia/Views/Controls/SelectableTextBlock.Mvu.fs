namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuSelectableTextBlock =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboardInit: bool

    static member CopyingToClipboard =
        if not MvuSelectableTextBlock._CopyingToClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSelectableTextBlock._CopyingToClipboardInit then
                    MvuSelectableTextBlock._CopyingToClipboard <-
                        Attributes.Mvu.defineEvent "SelectableTextBlock_CopyingToClipboard" (fun target -> (target :?> SelectableTextBlock).CopyingToClipboard)

                    MvuSelectableTextBlock._CopyingToClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSelectableTextBlock._CopyingToClipboard

[<AutoOpen>]
module MvuSelectableTextBlockBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a SelectableTextBlock widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the user copies the text to the clipboard.</param>
        static member inline SelectableTextBlock(text: string, fn: RoutedEventArgs -> 'msg) =
            let s1 = TextBlock.Text.WithValue(text)
            let s2 = MvuSelectableTextBlock.CopyingToClipboard.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabSelectableTextBlock>(
                SelectableTextBlock.WidgetKey,
                &bundle
            )

        /// <summary>Creates a SelectableTextBlock widget.</summary>
        /// <param name="fn">Raised when the user copies the text to the clipboard.</param>
        static member inline SelectableTextBlock(fn: RoutedEventArgs -> 'msg) =
            let scalar = MvuSelectableTextBlock.CopyingToClipboard.WithValue(fn)
            let attr = TextBlock.Inlines
            CollectionBuilder<'msg, IFabSelectableTextBlock, IFabInline>(SelectableTextBlock.WidgetKey, attr, scalar)
