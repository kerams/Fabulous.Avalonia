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
type ComponentAutoCompleteBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Text: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<string, string>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TextInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Populating: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PopulatingEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PopulatingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Populated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.PopulatedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PopulatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropDownOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DropDownOpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SelectionChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChangedInit: bool

    static member Text =
        if not ComponentAutoCompleteBox._TextInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentAutoCompleteBox._TextInit then
                    ComponentAutoCompleteBox._Text <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "AutoCompleteBox_TextChanged" AutoCompleteBox.TextProperty

                    ComponentAutoCompleteBox._TextInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentAutoCompleteBox._Text

    static member Populating =
        if not ComponentAutoCompleteBox._PopulatingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentAutoCompleteBox._PopulatingInit then
                    ComponentAutoCompleteBox._Populating <-
                        Attributes.Component.defineEvent<PopulatingEventArgs> "AutoCompleteBox_Populating" (fun target -> (target :?> AutoCompleteBox).Populating)

                    ComponentAutoCompleteBox._PopulatingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentAutoCompleteBox._Populating

    static member Populated =
        if not ComponentAutoCompleteBox._PopulatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentAutoCompleteBox._PopulatedInit then
                    ComponentAutoCompleteBox._Populated <-
                        Attributes.Component.defineEvent<PopulatedEventArgs> "AutoCompleteBox_Populated" (fun target -> (target :?> AutoCompleteBox).Populated)

                    ComponentAutoCompleteBox._PopulatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentAutoCompleteBox._Populated

    static member DropDownOpened =
        if not ComponentAutoCompleteBox._DropDownOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentAutoCompleteBox._DropDownOpenedInit then
                    ComponentAutoCompleteBox._DropDownOpened <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "AutoCompleteBox_onDropDownOpened" AutoCompleteBox.IsDropDownOpenProperty

                    ComponentAutoCompleteBox._DropDownOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentAutoCompleteBox._DropDownOpened

    static member SelectionChanged =
        if not ComponentAutoCompleteBox._SelectionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentAutoCompleteBox._SelectionChangedInit then
                    ComponentAutoCompleteBox._SelectionChanged <-
                        Attributes.Component.defineEvent<SelectionChangedEventArgs> "AutoCompleteBox_SelectionChanged" (fun target ->
                            (target :?> AutoCompleteBox).SelectionChanged)

                    ComponentAutoCompleteBox._SelectionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentAutoCompleteBox._SelectionChanged

type ComponentAutoCompleteBoxModifiers =
    /// <summary>Binds the AutoCompleteBox.TextProperty.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The value to bind.</param>
    /// <param name="fn">A function mapping the updated text to a 'msg to raise on user change.</param>
    [<Extension>]
    static member inline onTextChanged(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, value: string, fn: string -> unit) =
        this.AddScalar(ComponentAutoCompleteBox.Text.WithValue(ComponentValueEventData.create value fn))

    [<Extension>]
    static member inline onPopulating(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: PopulatingEventArgs -> unit) =
        this.AddScalar(ComponentAutoCompleteBox.Populating.WithValue(fn))

    /// <summary>Listens to the AutoCompleteBox Populated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the AutoCompleteBox Populated event is fired.</param>
    [<Extension>]
    static member inline onPopulated(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: PopulatedEventArgs -> unit) =
        this.AddScalar(ComponentAutoCompleteBox.Populated.WithValue(fn))

    /// <summary>Listens to the AutoCompleteBox DropDownOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="isOpen">The IsOpen value.</param>
    /// <param name="fn">Raised when the AutoCompleteBox DropDownOpened event is fired.</param>
    [<Extension>]
    static member inline onDropDownOpened(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, isOpen: bool, fn: bool -> unit) =
        this.AddScalar(ComponentAutoCompleteBox.DropDownOpened.WithValue(ComponentValueEventData.create isOpen fn))

    /// <summary>Listens to the AutoCompleteBox SelectionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the AutoCompleteBox SelectionChanged event is fired.</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabAutoCompleteBox>, fn: SelectionChangedEventArgs -> unit) =
        this.AddScalar(ComponentAutoCompleteBox.SelectionChanged.WithValue(fn))
