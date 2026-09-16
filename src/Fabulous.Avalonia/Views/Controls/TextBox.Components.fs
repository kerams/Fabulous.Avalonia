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
type ComponentTextBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<string, string>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CopyingToClipboardInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CuttingToClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CuttingToClipboardInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PastingFromClipboard: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PastingFromClipboardInit: bool

    static member TextChanged =
        if not ComponentTextBox._TextChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTextBox._TextChangedInit then
                    ComponentTextBox._TextChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "TextBox_TextChanged" TextBox.TextProperty

                    ComponentTextBox._TextChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTextBox._TextChanged

    static member CopyingToClipboard =
        if not ComponentTextBox._CopyingToClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTextBox._CopyingToClipboardInit then
                    ComponentTextBox._CopyingToClipboard <-
                        Attributes.Component.defineEvent<RoutedEventArgs> "TextBox_CopyingToClipboardEvent" (fun target -> (target :?> TextBox).CopyingToClipboard)

                    ComponentTextBox._CopyingToClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTextBox._CopyingToClipboard

    static member CuttingToClipboard =
        if not ComponentTextBox._CuttingToClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTextBox._CuttingToClipboardInit then
                    ComponentTextBox._CuttingToClipboard <-
                        Attributes.Component.defineEvent<RoutedEventArgs> "TextBox_CuttingToClipboard" (fun target -> (target :?> TextBox).CuttingToClipboard)

                    ComponentTextBox._CuttingToClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTextBox._CuttingToClipboard

    static member PastingFromClipboard =
        if not ComponentTextBox._PastingFromClipboardInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTextBox._PastingFromClipboardInit then
                    ComponentTextBox._PastingFromClipboard <-
                        Attributes.Component.defineEvent<RoutedEventArgs> "TextBox_PastingFromClipboardEvent" (fun target -> (target :?> TextBox).PastingFromClipboard)

                    ComponentTextBox._PastingFromClipboardInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTextBox._PastingFromClipboard

[<AutoOpen>]
module ComponentTextBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TextBox widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the text changes.</param>
        static member inline TextBox(text: string, fn: string -> unit) =
            let attr = ComponentTextBox.TextChanged.WithValue(ComponentValueEventData.create text fn)
            WidgetBuilder<'msg, IFabTextBox>(TextBox.WidgetKey, &attr)

type ComponentTextBoxModifiers =
    /// /// <summary>Listens to the TexBox CopyingToClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the CopyingToClipboard changes.</param>
    [<Extension>]
    static member inline onCopyingToClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentTextBox.CopyingToClipboard.WithValue(fn))

    /// <summary>Listens to the TexBox CuttingToClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the CuttingToClipboard changes.</param>
    [<Extension>]
    static member inline onCuttingToClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentTextBox.CuttingToClipboard.WithValue(fn))

    /// <summary>Listens to the TexBox PastingFromClipboard event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PastingFromClipboard changes.</param>
    [<Extension>]
    static member inline onPastingFromClipboard(this: WidgetBuilder<'msg, #IFabTextBox>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentTextBox.PastingFromClipboard.WithValue(fn))
