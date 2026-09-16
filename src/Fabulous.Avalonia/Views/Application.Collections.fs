namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections
open Fabulous.StackAllocatedCollections.StackList

// Separate file from Application.fs: F# initializes all top-level values of a file together, so touching
// Application.MainView (SingleViewApplication) would otherwise root TrayIcon and Window.
// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ApplicationCollections =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TrayIcons: Fabulous.WidgetCollectionAttributeDefinitions.WidgetCollectionAttributeDefinition

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _TrayIconsInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Windows: Fabulous.WidgetCollectionAttributeDefinitions.WidgetCollectionAttributeDefinition

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WindowsInit: bool

    static member TrayIcons =
        if not ApplicationCollections._TrayIconsInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ApplicationCollections._TrayIconsInit then
                    ApplicationCollections._TrayIcons <-
                        Attributes.defineAvaloniaListWidgetCollection "TrayIcon_TrayIcons" (fun target ->
                            let target = target :?> FabApplication
                            let trayIcons = TrayIcon.GetIcons(target)

                            if isNull trayIcons then
                                let trayIcons = TrayIcons()
                                TrayIcon.SetIcons(target, trayIcons)
                                trayIcons
                            else
                                trayIcons)

                    ApplicationCollections._TrayIconsInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ApplicationCollections._TrayIcons

    static member Windows =
        if not ApplicationCollections._WindowsInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ApplicationCollections._WindowsInit then
                    ApplicationCollections._Windows <-
                        Attributes.defineAvaloniaListWidgetCollectionWithCustomDiff<FabWindow>
                            "Application_Windows"
                            (fun target -> (target :?> FabApplication).InternalWindows)
                            (fun target _ view ->
                                let app = target :?> FabApplication
                                let window = view :?> FabWindow
                                app.AddWindow(window))
                            (fun target _ view ->
                                let app = target :?> FabApplication
                                let window = view :?> FabWindow
                                app.RemoveWindow(window))
                            (fun target _ oldView newView ->
                                let app = target :?> FabApplication
                                let oldWindow = oldView :?> FabWindow
                                let newWindow = newView :?> FabWindow
                                app.RemoveWindow(oldWindow)
                                app.AddWindow(newWindow))

                    ApplicationCollections._WindowsInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ApplicationCollections._Windows

[<AutoOpen>]
module ApplicationCollectionBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a DesktopApplication widget with a content widget.</summary>
        static member DesktopApplication() =
            let attr = ApplicationCollections.Windows
            let bundle = AttributesBundle(StackList.empty(), [||], [||])
            CollectionBuilder<'msg, IFabApplication, IFabWindow>(Application.WidgetKey, attr, bundle)

type ApplicationYieldExtensions =
    [<Extension>]
    static member inline Yield(_: AttributeCollectionBuilder<'msg, #IFabApplication, IFabTrayIcon>, x: WidgetBuilder<'msg, #IFabTrayIcon>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (_: AttributeCollectionBuilder<'msg, #IFabApplication, IFabTrayIcon>, x: WidgetBuilder<'msg, Memo.Memoized<#IFabTrayIcon>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'marker :> IFabApplication and 'itemType :> IFabWindow>
        (_: CollectionBuilder<'msg, 'marker, IFabWindow>, x: WidgetBuilder<'msg, 'itemType>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'msg: equality and 'marker :> IFabApplication and 'itemType :> IFabWindow>
        (_: CollectionBuilder<'msg, 'marker, IFabWindow>, x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

type TrayIconAttachedModifiers =
    /// <summary>Sets the tray icons for the application.</summary>
    /// <param name="this">Current widget.</param>
    [<Extension>]
    static member inline trayIcons<'msg, 'marker when 'msg: equality and 'marker :> IFabApplication>(this: WidgetBuilder<'msg, 'marker>) =
        let attr = ApplicationCollections.TrayIcons
        AttributeCollectionBuilder<'msg, 'marker, IFabTrayIcon>(&this, &attr)

    /// <summary>Sets the tray icon for the application.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="trayIcon">The TrayIcon value</param>
    [<Extension>]
    static member inline trayIcon(this: WidgetBuilder<'msg, #IFabApplication>, trayIcon: WidgetBuilder<'msg, IFabTrayIcon>) =
        let attr = ApplicationCollections.TrayIcons
        AttributeCollectionBuilder<'msg, #IFabApplication, IFabTrayIcon>(&this, &attr) { trayIcon }
