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
type ComponentApplication =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<Avalonia.Styling.ThemeVariant, Avalonia.Styling.ThemeVariant>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestedThemeChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<Avalonia.Styling.ThemeVariant, Avalonia.Styling.ThemeVariant>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _RequestedThemeChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResourcesChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ResourcesChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ResourcesChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Activated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ApplicationLifetimes.ActivatedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Deactivated: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ApplicationLifetimes.ActivatedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DeactivatedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SafeAreaChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.Platform.SafeAreaChangedArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SafeAreaChangedInit: bool

    static member ActualThemeVariantChanged =
        if not ComponentApplication._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._ActualThemeVariantChangedInit then
                    ComponentApplication._ActualThemeVariantChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "Application_ActualThemeVariantChanged" FabApplication.ActualThemeVariantProperty

                    ComponentApplication._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._ActualThemeVariantChanged

    static member RequestedThemeChanged =
        if not ComponentApplication._RequestedThemeChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._RequestedThemeChangedInit then
                    ComponentApplication._RequestedThemeChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "Application_RequestedThemeChanged" FabApplication.RequestedThemeVariantProperty

                    ComponentApplication._RequestedThemeChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._RequestedThemeChanged

    static member ResourcesChanged =
        if not ComponentApplication._ResourcesChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._ResourcesChangedInit then
                    ComponentApplication._ResourcesChanged <-
                        Attributes.Component.defineEvent "Application_ResourcesChangedEvent" (fun target -> (target :?> FabApplication).ResourcesChanged)

                    ComponentApplication._ResourcesChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._ResourcesChanged

    static member Activated =
        if not ComponentApplication._ActivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._ActivatedInit then
                    ComponentApplication._Activated <-
                        Attributes.Component.defineEvent "Application_Activated" (fun target ->
                            (FabApplication.Current.TryGetFeature(typeof<IActivatableLifetime>) :?> IActivatableLifetime)
                                .Activated)

                    ComponentApplication._ActivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._Activated

    static member Deactivated =
        if not ComponentApplication._DeactivatedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._DeactivatedInit then
                    ComponentApplication._Deactivated <-
                        Attributes.Component.defineEvent "Application_Deactivated" (fun target ->
                            (FabApplication.Current.TryGetFeature(typeof<IActivatableLifetime>) :?> IActivatableLifetime)
                                .Deactivated)

                    ComponentApplication._DeactivatedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._Deactivated

    static member SafeAreaChanged =
        if not ComponentApplication._SafeAreaChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentApplication._SafeAreaChangedInit then
                    ComponentApplication._SafeAreaChanged <-
                        Attributes.Component.defineEvent "PlatformSettings_SafeAreaChanged" (fun target -> (target :?> FabApplication).InsetsManager.SafeAreaChanged)

                    ComponentApplication._SafeAreaChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentApplication._SafeAreaChanged

type ComponentApplicationModifiers =
    /// <summary>Listens to the application ActualThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The new theme variant.</param>
    /// <param name="fn">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabApplication>, value: ThemeVariant, fn: ThemeVariant -> unit) =
        this.AddScalar(ComponentApplication.ActualThemeVariantChanged.WithValue(ComponentValueEventData.create value fn))

    /// <summary>Listens to the application RequestedThemeChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The new theme variant.</param>
    /// <param name="fn">Raised when the requested theme variant changes.</param>
    [<Extension>]
    static member inline onRequestedThemeChanged(this: WidgetBuilder<'msg, #IFabApplication>, value: ThemeVariant, fn: ThemeVariant -> unit) =
        this.AddScalar(ComponentApplication.RequestedThemeChanged.WithValue(ComponentValueEventData.create value fn))

    /// <summary>Listens to the application resources changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the resources change.</param>
    [<Extension>]
    static member inline onResourcesChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: ResourcesChangedEventArgs -> unit) =
        this.AddScalar(ComponentApplication.ResourcesChanged.WithValue(fn))

    /// <summary>Listens to the application activated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the application is activated.</param>
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<'msg, #IFabApplication>, fn: ActivatedEventArgs -> unit) =
        this.AddScalar(ComponentApplication.Activated.WithValue(fn))

    /// <summary>Listens to the application deactivated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the application is deactivated.</param>
    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabApplication>, fn: ActivatedEventArgs -> unit) =
        this.AddScalar(ComponentApplication.Deactivated.WithValue(fn))

    /// <summary>Listens to the PlatformSettings safe area changed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the safe area is changed.</param>
    [<Extension>]
    static member inline onSafeAreaChanged(this: WidgetBuilder<'msg, #IFabApplication>, fn: Platform.SafeAreaChangedArgs -> unit) =
        this.AddScalar(ComponentApplication.SafeAreaChanged.WithValue(fn))
