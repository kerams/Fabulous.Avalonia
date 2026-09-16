namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuMenuItem =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Click: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClickInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerEnteredItem: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerEnteredItemInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerExitedItem: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PointerExitedItemInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SubmenuOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SubmenuOpenedInit: bool

    static member Click =
        if not MvuMenuItem._ClickInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuItem._ClickInit then
                    MvuMenuItem._Click <-
                        Attributes.Mvu.defineEvent "MenuItem_Clicked" (fun target -> (target :?> MenuItem).Click)

                    MvuMenuItem._ClickInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuItem._Click

    static member PointerEnteredItem =
        if not MvuMenuItem._PointerEnteredItemInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuItem._PointerEnteredItemInit then
                    MvuMenuItem._PointerEnteredItem <-
                        Attributes.Mvu.defineEvent "MenuItem_PointerEnteredItem" (fun target -> (target :?> MenuItem).PointerEnteredItem)

                    MvuMenuItem._PointerEnteredItemInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuItem._PointerEnteredItem

    static member PointerExitedItem =
        if not MvuMenuItem._PointerExitedItemInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuItem._PointerExitedItemInit then
                    MvuMenuItem._PointerExitedItem <-
                        Attributes.Mvu.defineEvent "MenuItem_PointerExitedItem" (fun target -> (target :?> MenuItem).PointerExitedItem)

                    MvuMenuItem._PointerExitedItemInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuItem._PointerExitedItem

    static member SubmenuOpened =
        if not MvuMenuItem._SubmenuOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuMenuItem._SubmenuOpenedInit then
                    MvuMenuItem._SubmenuOpened <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "MvuMenuItem_SubmenuOpened" MenuItem.IsSubMenuOpenProperty

                    MvuMenuItem._SubmenuOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuMenuItem._SubmenuOpened

type MvuMenuItemModifiers =
    /// <summary>Listens to the MenuItem PointerEnteredItem event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PointerEnteredItem event is fired.</param>
    [<Extension>]
    static member inline onPointerEnteredItem(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuMenuItem.PointerEnteredItem.WithValue(fn))

    /// <summary>Listens to the MenuItem PointerExitedItem event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PointerExitedItem event is fired.</param>
    [<Extension>]
    static member inline onPointerExitedItem(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuMenuItem.PointerExitedItem.WithValue(fn))

    /// <summary>Listens to the MenuItem SubmenuOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Whether the Submenu is open.</param>
    /// <param name="fn">Raised when the SubmenuOpened event is fired.</param>
    [<Extension>]
    static member inline onSubmenuOpened(this: WidgetBuilder<'msg, #IFabMenuItem>, value: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuMenuItem.SubmenuOpened.WithValue(ValueEventData.create value fn))

    /// <summary>Listens to the MenuItem Click event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the MenuItem is clicked.</param>
    [<Extension>]
    static member inline onClick(this: WidgetBuilder<'msg, #IFabMenuItem>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuMenuItem.Click.WithValue(fn))
