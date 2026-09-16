namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Avalonia.Input

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestBringIntoView: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.RequestBringIntoViewEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestBringIntoViewInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContextRequested: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Input.ContextRequestedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContextRequestedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Loaded: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _LoadedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _UnLoaded: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _UnLoadedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SizeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SizeChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SizeChangedInit: bool

    static member RequestBringIntoView =
        if not MvuControl._RequestBringIntoViewInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuControl._RequestBringIntoViewInit then
                    MvuControl._RequestBringIntoView <-
                        Attributes.Mvu.defineRoutedEvent "Control_RequestBringIntoView" Control.RequestBringIntoViewEvent

                    MvuControl._RequestBringIntoViewInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuControl._RequestBringIntoView

    static member ContextRequested =
        if not MvuControl._ContextRequestedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuControl._ContextRequestedInit then
                    MvuControl._ContextRequested <-
                        Attributes.Mvu.defineEvent "Control_ContextRequested" (fun target -> (target :?> Control).ContextRequested)

                    MvuControl._ContextRequestedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuControl._ContextRequested

    static member Loaded =
        if not MvuControl._LoadedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuControl._LoadedInit then
                    MvuControl._Loaded <-
                        Attributes.Mvu.defineEvent "Control_Loaded" (fun target -> (target :?> Control).Loaded)

                    MvuControl._LoadedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuControl._Loaded

    static member UnLoaded =
        if not MvuControl._UnLoadedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuControl._UnLoadedInit then
                    MvuControl._UnLoaded <-
                        Attributes.Mvu.defineEvent "Control_UnLoaded" (fun target -> (target :?> Control).Unloaded)

                    MvuControl._UnLoadedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuControl._UnLoaded

    static member SizeChanged =
        if not MvuControl._SizeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuControl._SizeChangedInit then
                    MvuControl._SizeChanged <-
                        Attributes.Mvu.defineEvent "Control_SizeChanged" (fun target -> (target :?> Control).SizeChanged)

                    MvuControl._SizeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuControl._SizeChanged

type MvuControlModifiers =
    /// <summary>Listens to the Control ContextRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the user has completed a context input gesture, such as a right-click.</param>
    [<Extension>]
    static member inline onContextRequested(this: WidgetBuilder<'msg, #IFabControl>, fn: ContextRequestedEventArgs -> 'msg) =
        this.AddScalar(MvuControl.ContextRequested.WithValue(fn))

    /// <summary>Listens to the Control RequestBringIntoView event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when an element wishes to be scrolled into view.</param>
    [<Extension>]
    static member inline onRequestBringIntoView(this: WidgetBuilder<'msg, #IFabControl>, fn: RequestBringIntoViewEventArgs -> 'msg) =
        this.AddScalar(MvuControl.RequestBringIntoView.WithValue(fn))

    /// <summary>Listens to the Control Loaded event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control has been fully constructed in the visual tree and both
    /// layout and render are complete.</param>
    [<Extension>]
    static member inline onLoaded(this: WidgetBuilder<'msg, #IFabControl>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuControl.Loaded.WithValue(fn))

    /// <summary>Listens to the Control UnLoaded event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control is removed from the visual tree.</param>
    [<Extension>]
    static member inline onUnLoaded(this: WidgetBuilder<'msg, #IFabControl>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuControl.UnLoaded.WithValue(fn))

    /// <summary>Listens to the Control SizeChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control's size changes.</param>
    [<Extension>]
    static member inline onSizeChanged(this: WidgetBuilder<'msg, #IFabControl>, fn: SizeChangedEventArgs -> 'msg) =
        this.AddScalar(MvuControl.SizeChanged.WithValue(fn))
