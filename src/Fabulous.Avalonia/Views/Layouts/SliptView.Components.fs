namespace Fabulous.Avalonia

open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open System.Runtime.CompilerServices

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentSplitView =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsPresented: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsPresentedInit: bool

    static member PanClosed =
        if not ComponentSplitView._PanClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSplitView._PanClosedInit then
                    ComponentSplitView._PanClosed <-
                        Attributes.Component.defineEvent "SplitView_PanClosed" (fun target -> (target :?> SplitView).PaneClosed)

                    ComponentSplitView._PanClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSplitView._PanClosed

    static member PanClosing =
        if not ComponentSplitView._PanClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSplitView._PanClosingInit then
                    ComponentSplitView._PanClosing <-
                        Attributes.Component.defineEvent "SplitView_PanClosing" (fun target -> (target :?> SplitView).PaneClosing)

                    ComponentSplitView._PanClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSplitView._PanClosing

    static member PanOpened =
        if not ComponentSplitView._PanOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSplitView._PanOpenedInit then
                    ComponentSplitView._PanOpened <-
                        Attributes.Component.defineEvent "SplitView_PanOpened" (fun target -> (target :?> SplitView).PaneOpened)

                    ComponentSplitView._PanOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSplitView._PanOpened

    static member PanOpening =
        if not ComponentSplitView._PanOpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSplitView._PanOpeningInit then
                    ComponentSplitView._PanOpening <-
                        Attributes.Component.defineEvent "SplitView_PanOpening" (fun target -> (target :?> SplitView).PaneOpening)

                    ComponentSplitView._PanOpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSplitView._PanOpening

    static member IsPresented =
        if not ComponentSplitView._IsPresentedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentSplitView._IsPresentedInit then
                    ComponentSplitView._IsPresented <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "SplitView_IsPresented" SplitView.IsPaneOpenProperty

                    ComponentSplitView._IsPresentedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentSplitView._IsPresented

type ComponentSplitViewModifiers =
    /// <summary>Listens to the SplitView PanClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanClosed event fires.</param>
    [<Extension>]
    static member inline onPanClosed(this: WidgetBuilder<'msg, #IFabSplitView>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentSplitView.PanClosed.WithValue(fn))

    /// <summary>Listens to the SplitView PanClosing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanClosing event fires.</param>
    [<Extension>]
    static member inline onPanClosing(this: WidgetBuilder<'msg, #IFabSplitView>, fn: CancelRoutedEventArgs -> unit) =
        this.AddScalar(ComponentSplitView.PanClosing.WithValue(fn))

    /// <summary>Listens to the SplitView PanOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanOpened event fires.</param>
    [<Extension>]
    static member inline onPanOpened(this: WidgetBuilder<'msg, #IFabSplitView>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentSplitView.PanOpened.WithValue(fn))

    /// <summary>Listens to the SplitView PanOpening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanOpening event fires.</param>
    [<Extension>]
    static member inline onPanOpening(this: WidgetBuilder<'msg, #IFabSplitView>, fn: CancelRoutedEventArgs -> unit) =
        this.AddScalar(ComponentSplitView.PanOpening.WithValue(fn))

    /// <summary>Listens to the SplitView IsPresented event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsPresented value.</param>
    /// <param name="fn">Raised when the IsPresented event fires.</param>
    [<Extension>]
    static member inline isPresented(this: WidgetBuilder<'msg, #IFabSplitView>, value: bool, fn: bool -> unit) =
        this.AddScalar(ComponentSplitView.IsPresented.WithValue(ComponentValueEventData.create value fn))
