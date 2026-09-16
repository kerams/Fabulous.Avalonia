namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuAutoCompleteBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Text: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<string, string>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Populating: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PopulatingEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PopulatingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Populated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PopulatedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PopulatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropDownOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropDownOpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SelectionChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChangedInit: bool

    static member Text =
        if not MvuAutoCompleteBox._TextInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAutoCompleteBox._TextInit then
                    MvuAutoCompleteBox._Text <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "AutoCompleteBox_TextChanged" AutoCompleteBox.TextProperty

                    MvuAutoCompleteBox._TextInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAutoCompleteBox._Text

    static member Populating =
        if not MvuAutoCompleteBox._PopulatingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAutoCompleteBox._PopulatingInit then
                    MvuAutoCompleteBox._Populating <-
                        Attributes.Mvu.defineEvent<PopulatingEventArgs> "AutoCompleteBox_Populating" (fun target -> (target :?> AutoCompleteBox).Populating)

                    MvuAutoCompleteBox._PopulatingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAutoCompleteBox._Populating

    static member Populated =
        if not MvuAutoCompleteBox._PopulatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAutoCompleteBox._PopulatedInit then
                    MvuAutoCompleteBox._Populated <-
                        Attributes.Mvu.defineEvent<PopulatedEventArgs> "AutoCompleteBox_Populated" (fun target -> (target :?> AutoCompleteBox).Populated)

                    MvuAutoCompleteBox._PopulatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAutoCompleteBox._Populated

    static member DropDownOpened =
        if not MvuAutoCompleteBox._DropDownOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAutoCompleteBox._DropDownOpenedInit then
                    MvuAutoCompleteBox._DropDownOpened <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "AutoCompleteBox_onDropDownOpened" AutoCompleteBox.IsDropDownOpenProperty

                    MvuAutoCompleteBox._DropDownOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAutoCompleteBox._DropDownOpened

    static member SelectionChanged =
        if not MvuAutoCompleteBox._SelectionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuAutoCompleteBox._SelectionChangedInit then
                    MvuAutoCompleteBox._SelectionChanged <-
                        Attributes.Mvu.defineEvent<SelectionChangedEventArgs> "AutoCompleteBox_SelectionChanged" (fun target -> (target :?> AutoCompleteBox).SelectionChanged)

                    MvuAutoCompleteBox._SelectionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuAutoCompleteBox._SelectionChanged

type MvuAutoCompleteBoxModifiers =
    /// <summary>Binds the AutoCompleteBox.TextProperty.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The value to bind.</param>
    /// <param name="fn">A function mapping the updated text to a 'msg to raise on user change.</param>
    [<Extension>]
    static member inline onTextChanged(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, value: string, fn: string -> 'msg) =
        this.AddScalar(MvuAutoCompleteBox.Text.WithValue(ValueEventData.create value fn))

    [<Extension>]
    static member inline onPopulating(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: PopulatingEventArgs -> 'msg) =
        this.AddScalar(MvuAutoCompleteBox.Populating.WithValue(fn))

    /// <summary>Listens to the AutoCompleteBox Populated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the AutoCompleteBox Populated event is fired.</param>
    [<Extension>]
    static member inline onPopulated(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: PopulatedEventArgs -> 'msg) =
        this.AddScalar(MvuAutoCompleteBox.Populated.WithValue(fn))

    /// <summary>Listens to the AutoCompleteBox DropDownOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="isOpen">The IsOpen value.</param>
    /// <param name="fn">Raised when the AutoCompleteBox DropDownOpened event is fired.</param>
    [<Extension>]
    static member inline onDropDownOpened(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, isOpen: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuAutoCompleteBox.DropDownOpened.WithValue(ValueEventData.create isOpen fn))

    /// <summary>Listens to the AutoCompleteBox SelectionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the AutoCompleteBox SelectionChanged event is fired.</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: SelectionChangedEventArgs -> 'msg) =
        this.AddScalar(MvuAutoCompleteBox.SelectionChanged.WithValue(fn))
