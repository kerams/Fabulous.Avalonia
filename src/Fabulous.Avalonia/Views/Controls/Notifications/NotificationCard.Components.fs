namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Notifications
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentNotificationCard =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _NotificationClosed: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.RoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _NotificationClosedInit: bool

    static member NotificationClosed =
        if not ComponentNotificationCard._NotificationClosedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNotificationCard._NotificationClosedInit then
                    ComponentNotificationCard._NotificationClosed <-
                        Attributes.Component.defineRoutedEvent "NotificationCard_NotificationClosed" NotificationCard.NotificationClosedEvent

                    ComponentNotificationCard._NotificationClosedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNotificationCard._NotificationClosed

type ComponentNotificationCardModifiers =
    /// <summary>Listens to the NotificationCard NotificationClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the NotificationCard is closed.</param>
    [<Extension>]
    static member inline onNotificationClosed(this: WidgetBuilder<'msg, #IFabNotificationCard>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentNotificationCard.NotificationClosed.WithValue(fn))
