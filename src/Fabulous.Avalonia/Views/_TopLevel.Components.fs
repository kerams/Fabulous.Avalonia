namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentTopLevel =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Opened: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _OpenedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Closed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClosedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScalingChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScalingChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _BackRequested: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _BackRequestedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    static member Opened =
        if not ComponentTopLevel._OpenedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTopLevel._OpenedInit then
                    ComponentTopLevel._Opened <-
                        Attributes.Component.defineEventNoArg "TopLevel_OpenedEvent" (fun target -> (target :?> TopLevel).Opened)

                    ComponentTopLevel._OpenedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTopLevel._Opened

    static member Closed =
        if not ComponentTopLevel._ClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTopLevel._ClosedInit then
                    ComponentTopLevel._Closed <-
                        Attributes.Component.defineEventNoArg "TopLevel_ClosedEvent" (fun target -> (target :?> TopLevel).Closed)

                    ComponentTopLevel._ClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTopLevel._Closed

    static member ScalingChanged =
        if not ComponentTopLevel._ScalingChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTopLevel._ScalingChangedInit then
                    ComponentTopLevel._ScalingChanged <-
                        Attributes.Component.defineEventNoArg "TopLevel_ScalingChangedEvent" (fun target -> (target :?> TopLevel).ScalingChanged)

                    ComponentTopLevel._ScalingChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTopLevel._ScalingChanged

    static member BackRequested =
        if not ComponentTopLevel._BackRequestedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTopLevel._BackRequestedInit then
                    ComponentTopLevel._BackRequested <-
                        Attributes.Component.defineEvent "TopLevel_BackRequestedEvent" (fun target -> (target :?> TopLevel).BackRequested)

                    ComponentTopLevel._BackRequestedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTopLevel._BackRequested

    static member ActualThemeVariantChanged =
        if not ComponentTopLevel._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTopLevel._ActualThemeVariantChangedInit then
                    ComponentTopLevel._ActualThemeVariantChanged <-
                        Attributes.Component.defineEventNoArg "TopLevel_ThemeVariantChanged" (fun target -> (target :?> TopLevel).ActualThemeVariantChanged)

                    ComponentTopLevel._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTopLevel._ActualThemeVariantChanged

type ComponentTopLevelModifiers =
    /// <summary>Listens to the TopLevel ThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabTopLevel>, fn: unit -> unit) =
        this.AddScalar(ComponentTopLevel.ActualThemeVariantChanged.WithValue(fn))

    /// <summary>Listens the TopLevel Opened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is opened.</param>
    [<Extension>]
    static member inline onOpened(this: WidgetBuilder<'msg, #IFabTopLevel>, fn: unit -> unit) =
        this.AddScalar(ComponentTopLevel.Opened.WithValue(fn))

    /// <summary>Listens the TopLevel Closed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closed.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabTopLevel>, fn: unit -> unit) =
        this.AddScalar(ComponentTopLevel.Closed.WithValue(fn))

    /// <summary>Listens the TopLevel BackRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the back button is pressed.</param>
    [<Extension>]
    static member inline onBackRequested(this: WidgetBuilder<'msg, #IFabTopLevel>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentTopLevel.BackRequested.WithValue(fn))

    /// <summary>Listens the TopLevel ScalingChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the TopLevel's scaling changes.</param>
    [<Extension>]
    static member inline onScalingChanged(this: WidgetBuilder<'msg, #IFabTopLevel>, fn: unit -> unit) =
        this.AddScalar(ComponentTopLevel.ScalingChanged.WithValue(fn))
