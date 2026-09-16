namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentMenuItem =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Click: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClickInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerEnteredItem: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerEnteredItemInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerExitedItem: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerExitedItemInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SubmenuOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SubmenuOpenedInit: bool

    static member Click =
        if not ComponentMenuItem._ClickInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuItem._ClickInit then
                    ComponentMenuItem._Click <-
                        Attributes.Component.defineEvent "MenuItem_Clicked" (fun target -> (target :?> MenuItem).Click)

                    ComponentMenuItem._ClickInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuItem._Click

    static member PointerEnteredItem =
        if not ComponentMenuItem._PointerEnteredItemInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuItem._PointerEnteredItemInit then
                    ComponentMenuItem._PointerEnteredItem <-
                        Attributes.Component.defineEvent "MenuItem_PointerEnteredItem" (fun target -> (target :?> MenuItem).PointerEnteredItem)

                    ComponentMenuItem._PointerEnteredItemInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuItem._PointerEnteredItem

    static member PointerExitedItem =
        if not ComponentMenuItem._PointerExitedItemInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuItem._PointerExitedItemInit then
                    ComponentMenuItem._PointerExitedItem <-
                        Attributes.Component.defineEvent "MenuItem_PointerExitedItem" (fun target -> (target :?> MenuItem).PointerExitedItem)

                    ComponentMenuItem._PointerExitedItemInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuItem._PointerExitedItem

    static member SubmenuOpened =
        if not ComponentMenuItem._SubmenuOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentMenuItem._SubmenuOpenedInit then
                    ComponentMenuItem._SubmenuOpened <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "MvuMenuItem_SubmenuOpened" MenuItem.IsSubMenuOpenProperty

                    ComponentMenuItem._SubmenuOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentMenuItem._SubmenuOpened

type ComponentMenuItemModifiers =
    /// <summary>Listens to the MenuItem PointerEnteredItem event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PointerEnteredItem event is fired.</param>
    [<Extension>]
    static member inline onPointerEnteredItem(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentMenuItem.PointerEnteredItem.WithValue(fn))

    /// <summary>Listens to the MenuItem PointerExitedItem event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PointerExitedItem event is fired.</param>
    [<Extension>]
    static member inline onPointerExitedItem(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentMenuItem.PointerExitedItem.WithValue(fn))

    /// <summary>Listens to the MenuItem SubmenuOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Whether the Submenu is open.</param>
    /// <param name="fn">Raised when the SubmenuClosed event is fired.</param>
    [<Extension>]
    static member inline onSubmenuOpened(this: WidgetBuilder<'msg, #IFabMenuItem>, value: bool, fn: bool -> unit) =
        this.AddScalar(ComponentMenuItem.SubmenuOpened.WithValue(ComponentValueEventData.create value fn))

    /// <summary>Listens to the MenuItem Click event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the MenuItem is clicked.</param>
    [<Extension>]
    static member inline onClick(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentMenuItem.Click.WithValue(fn))
