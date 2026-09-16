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
type MvuSplitView =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanClosingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpening: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _PanOpeningInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsPresented: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsPresentedInit: bool

    static member PanClosed =
        if not MvuSplitView._PanClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitView._PanClosedInit then
                    MvuSplitView._PanClosed <-
                        Attributes.Mvu.defineEvent "SplitView_PanClosed" (fun target -> (target :?> SplitView).PaneClosed)

                    MvuSplitView._PanClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitView._PanClosed

    static member PanClosing =
        if not MvuSplitView._PanClosingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitView._PanClosingInit then
                    MvuSplitView._PanClosing <-
                        Attributes.Mvu.defineEvent "SplitView_PanClosing" (fun target -> (target :?> SplitView).PaneClosing)

                    MvuSplitView._PanClosingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitView._PanClosing

    static member PanOpened =
        if not MvuSplitView._PanOpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitView._PanOpenedInit then
                    MvuSplitView._PanOpened <-
                        Attributes.Mvu.defineEvent "SplitView_PanOpened" (fun target -> (target :?> SplitView).PaneOpened)

                    MvuSplitView._PanOpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitView._PanOpened

    static member PanOpening =
        if not MvuSplitView._PanOpeningInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitView._PanOpeningInit then
                    MvuSplitView._PanOpening <-
                        Attributes.Mvu.defineEvent "SplitView_PanOpening" (fun target -> (target :?> SplitView).PaneOpening)

                    MvuSplitView._PanOpeningInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitView._PanOpening

    static member IsPresented =
        if not MvuSplitView._IsPresentedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuSplitView._IsPresentedInit then
                    MvuSplitView._IsPresented <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "SplitView_IsPresented" SplitView.IsPaneOpenProperty

                    MvuSplitView._IsPresentedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuSplitView._IsPresented

type MvuSplitViewModifiers =
    /// <summary>Listens to the SplitView PanClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanClosed event fires.</param>
    [<Extension>]
    static member inline onPanClosed(this: WidgetBuilder<'msg, #IFabSplitView>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuSplitView.PanClosed.WithValue(fn))

    /// <summary>Listens to the SplitView PanClosing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanClosing event fires.</param>
    [<Extension>]
    static member inline onPanClosing(this: WidgetBuilder<'msg, #IFabSplitView>, fn: CancelRoutedEventArgs -> 'msg) =
        this.AddScalar(MvuSplitView.PanClosing.WithValue(fn))

    /// <summary>Listens to the SplitView PanOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanOpened event fires.</param>
    [<Extension>]
    static member inline onPanOpened(this: WidgetBuilder<'msg, #IFabSplitView>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuSplitView.PanOpened.WithValue(fn))

    /// <summary>Listens to the SplitView PanOpening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the PanOpening event fires.</param>
    [<Extension>]
    static member inline onPanOpening(this: WidgetBuilder<'msg, #IFabSplitView>, fn: CancelRoutedEventArgs -> 'msg) =
        this.AddScalar(MvuSplitView.PanOpening.WithValue(fn))

    /// <summary>Listens to the SplitView IsPresented event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsPresented value.</param>
    /// <param name="fn">Raised when the IsPresented event fires.</param>
    [<Extension>]
    static member inline isPresented(this: WidgetBuilder<'msg, #IFabSplitView>, value: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuSplitView.IsPresented.WithValue(ValueEventData.create value fn))
