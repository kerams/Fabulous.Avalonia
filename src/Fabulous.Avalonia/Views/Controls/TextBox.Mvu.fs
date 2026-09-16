namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Avalonia.Interactivity
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTextBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<string, string>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboardInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CuttingToClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CuttingToClipboardInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PastingFromClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PastingFromClipboardInit: bool

    static member TextChanged =
        if not MvuTextBox._TextChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTextBox._TextChangedInit then
                    MvuTextBox._TextChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "TextBox_TextChanged" TextBox.TextProperty

                    MvuTextBox._TextChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTextBox._TextChanged

    static member CopyingToClipboard =
        if not MvuTextBox._CopyingToClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTextBox._CopyingToClipboardInit then
                    MvuTextBox._CopyingToClipboard <-
                        Attributes.Mvu.defineEvent<RoutedEventArgs> "TextBox_CopyingToClipboardEvent" (fun target -> (target :?> TextBox).CopyingToClipboard)

                    MvuTextBox._CopyingToClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTextBox._CopyingToClipboard

    static member CuttingToClipboard =
        if not MvuTextBox._CuttingToClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTextBox._CuttingToClipboardInit then
                    MvuTextBox._CuttingToClipboard <-
                        Attributes.Mvu.defineEvent<RoutedEventArgs> "TextBox_CuttingToClipboard" (fun target -> (target :?> TextBox).CuttingToClipboard)

                    MvuTextBox._CuttingToClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTextBox._CuttingToClipboard

    static member PastingFromClipboard =
        if not MvuTextBox._PastingFromClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTextBox._PastingFromClipboardInit then
                    MvuTextBox._PastingFromClipboard <-
                        Attributes.Mvu.defineEvent<RoutedEventArgs> "TextBox_PastingFromClipboardEvent" (fun target -> (target :?> TextBox).PastingFromClipboard)

                    MvuTextBox._PastingFromClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTextBox._PastingFromClipboard

[<AutoOpen>]
module MvuTextBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TextBox widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the text changes.</param>
        static member inline TextBox(text: string, fn: string -> 'msg) =
            let attr = MvuTextBox.TextChanged.WithValue(ValueEventData.create text fn)
            WidgetBuilder<'msg, IFabTextBox>(TextBox.WidgetKey, &attr)

type MvuTextBoxModifiers =
    /// /// <summary>Listens to the TexBox CopyingToClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the CopyingToClipboard changes.</param>
    [<Extension>]
    static member inline onCopyingToClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuTextBox.CopyingToClipboard.WithValue(fn))

    /// <summary>Listens to the TexBox CuttingToClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the CuttingToClipboard changes.</param>
    [<Extension>]
    static member inline onCuttingToClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuTextBox.CuttingToClipboard.WithValue(fn))

    /// <summary>Listens to the TexBox PastingFromClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PastingFromClipboard changes.</param>
    [<Extension>]
    static member inline onPastingFromClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuTextBox.PastingFromClipboard.WithValue(fn))
