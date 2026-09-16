namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuSplitButton =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Clicked: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClickedInit: bool

    static member Clicked =
        if not MvuSplitButton._ClickedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitButton._ClickedInit then
                    MvuSplitButton._Clicked <-
                        Attributes.Mvu.defineEvent "SplitButton_Clicked" (fun target -> (target :?> SplitButton).Click)

                    MvuSplitButton._ClickedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitButton._Clicked

[<AutoOpen>]
module MvuSplitButtonBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        static member SplitButton(text: string, fn: RoutedEventArgs -> 'msg) =
            let s1 = ContentControl.ContentString.WithValue(text)
            let s2 = MvuSplitButton.Clicked.WithValue(fn)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabSplitButton>(SplitButton.WidgetKey, &bundle)

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        /// <param name="content">The content to display in the flyout.</param>
        static member SplitButton(fn: RoutedEventArgs -> 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(MvuSplitButton.Clicked.WithValue(fn)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabSplitButton>(SplitButton.WidgetKey, &bundle)
