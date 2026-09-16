namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Styling
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuApplication =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<Avalonia.Styling.ThemeVariant, Avalonia.Styling.ThemeVariant>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestedThemeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<Avalonia.Styling.ThemeVariant, Avalonia.Styling.ThemeVariant>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestedThemeChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResourcesChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ResourcesChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResourcesChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Activated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ApplicationLifetimes.ActivatedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Deactivated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ApplicationLifetimes.ActivatedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DeactivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ColorValuesChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Platform.PlatformColorValues -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ColorValuesChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SafeAreaChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.Platform.SafeAreaChangedArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SafeAreaChangedInit: bool

    static member ActualThemeVariantChanged =
        if not MvuApplication._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._ActualThemeVariantChangedInit then
                    MvuApplication._ActualThemeVariantChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "Application_ActualThemeVariantChanged" FabApplication.ActualThemeVariantProperty

                    MvuApplication._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._ActualThemeVariantChanged

    static member RequestedThemeChanged =
        if not MvuApplication._RequestedThemeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._RequestedThemeChangedInit then
                    MvuApplication._RequestedThemeChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "Application_RequestedThemeChanged" FabApplication.RequestedThemeVariantProperty

                    MvuApplication._RequestedThemeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._RequestedThemeChanged

    static member ResourcesChanged =
        if not MvuApplication._ResourcesChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._ResourcesChangedInit then
                    MvuApplication._ResourcesChanged <-
                        Attributes.Mvu.defineEvent "Application_ResourcesChangedEvent" (fun target -> (target :?> FabApplication).ResourcesChanged)

                    MvuApplication._ResourcesChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._ResourcesChanged

    static member Activated =
        if not MvuApplication._ActivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._ActivatedInit then
                    MvuApplication._Activated <-
                        Attributes.Mvu.defineEvent "Application_Activated" (fun target ->
                            (FabApplication.Current.TryGetFeature(typeof<IActivatableLifetime>) :?> IActivatableLifetime)
                                .Activated)

                    MvuApplication._ActivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._Activated

    static member Deactivated =
        if not MvuApplication._DeactivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._DeactivatedInit then
                    MvuApplication._Deactivated <-
                        Attributes.Mvu.defineEvent "Application_Deactivated" (fun target ->
                            (FabApplication.Current.TryGetFeature(typeof<IActivatableLifetime>) :?> IActivatableLifetime)
                                .Deactivated)

                    MvuApplication._DeactivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._Deactivated

    static member ColorValuesChanged =
        if not MvuApplication._ColorValuesChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._ColorValuesChangedInit then
                    MvuApplication._ColorValuesChanged <-
                        Attributes.Mvu.defineEvent "PlatformSettings_ColorValuesChanged" (fun target ->
                            (target :?> FabApplication)
                                .PlatformSettings.ColorValuesChanged)

                    MvuApplication._ColorValuesChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._ColorValuesChanged

    static member SafeAreaChanged =
        if not MvuApplication._SafeAreaChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuApplication._SafeAreaChangedInit then
                    MvuApplication._SafeAreaChanged <-
                        Attributes.Mvu.defineEvent "PlatformSettings_SafeAreaChanged" (fun target -> (target :?> FabApplication).InsetsManager.SafeAreaChanged)

                    MvuApplication._SafeAreaChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuApplication._SafeAreaChanged

type MvuApplicationModifiers =
    /// <summary>Listens to the application ActualThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The new theme variant.</param>
    /// <param name="fn">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabApplication>, value: ThemeVariant, fn: ThemeVariant -> 'msg) =
        this.AddScalar(MvuApplication.ActualThemeVariantChanged.WithValue(ValueEventData.create value fn))

    /// <summary>Listens to the application RequestedThemeChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The new theme variant.</param>
    /// <param name="fn">Raised when the requested theme variant changes.</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, value: ThemeVariant, fn: ThemeVariant -> 'msg) =
        this.AddScalar(MvuApplication.RequestedThemeChanged.WithValue(ValueEventData.create value fn))

    /// <summary>Listens to the application resources changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the resources change.</param>
    [<Extension>]
    static member inline onResourcesChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: ResourcesChangedEventArgs -> 'msg) =
        this.AddScalar(MvuApplication.ResourcesChanged.WithValue(fn))

    /// <summary>Listens to the application activated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the application is activated.</param>
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabApplication>, fn: ActivatedEventArgs -> 'msg) =
        this.AddScalar(MvuApplication.Activated.WithValue(fn))

    /// <summary>Listens to the application deactivated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the application is deactivated.</param>
    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabApplication>, fn: ActivatedEventArgs -> 'msg) =
        this.AddScalar(MvuApplication.Deactivated.WithValue(fn))

    /// <summary>Listens to the PlatformSettings color values changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when current system color values are changed. Including changing of a dark mode and accent colors.</param>
    [<Extension>]
    static member inline onColorValuesChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: Platform.PlatformColorValues -> 'msg) =
        this.AddScalar(MvuApplication.ColorValuesChanged.WithValue(fn))

    /// <summary>Listens to the PlatformSettings safe area changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the safe area is changed.</param>
    [<Extension>]
    static member inline onSafeAreaChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: Platform.SafeAreaChangedArgs -> 'msg) =
        this.AddScalar(MvuApplication.SafeAreaChanged.WithValue(fn))
