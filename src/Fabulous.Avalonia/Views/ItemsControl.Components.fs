namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentItemsControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerClearing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerClearingEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerClearingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerIndexChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerIndexChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerIndexChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerPrepared: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerPreparedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerPreparedInit: bool

    static member ContainerClearing =
        if not ComponentItemsControl._ContainerClearingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentItemsControl._ContainerClearingInit then
                    ComponentItemsControl._ContainerClearing <-
                        Attributes.Component.defineEvent "ItemsControl_ContainerClearing" (fun target -> (target :?> ItemsControl).ContainerClearing)

                    ComponentItemsControl._ContainerClearingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentItemsControl._ContainerClearing

    static member ContainerIndexChanged =
        if not ComponentItemsControl._ContainerIndexChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentItemsControl._ContainerIndexChangedInit then
                    ComponentItemsControl._ContainerIndexChanged <-
                        Attributes.Component.defineEvent "ItemsControl_ContainerIndexChanged" (fun target -> (target :?> ItemsControl).ContainerIndexChanged)

                    ComponentItemsControl._ContainerIndexChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentItemsControl._ContainerIndexChanged

    static member ContainerPrepared =
        if not ComponentItemsControl._ContainerPreparedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentItemsControl._ContainerPreparedInit then
                    ComponentItemsControl._ContainerPrepared <-
                        Attributes.Component.defineEvent "ItemsControl_ContainerPrepared" (fun target -> (target :?> ItemsControl).ContainerPrepared)

                    ComponentItemsControl._ContainerPreparedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentItemsControl._ContainerPrepared

type ComponentItemsControlModifiers =
    /// <summary>Listens to the ItemsControl ContainerClearing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onContainerClearing(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerClearingEventArgs -> unit) =
        this.AddScalar(ComponentItemsControl.ContainerClearing.WithValue(fn))

    /// <summary>Listens to the ItemsControl ContainerIndexChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the index for the item it represents has changed.</param>
    [<Extension>]
    static member inline onContainerIndexChanged(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerIndexChangedEventArgs -> unit) =
        this.AddScalar(ComponentItemsControl.ContainerIndexChanged.WithValue(fn))

    /// <summary>Listens to the ItemsControl ContainerPrepared event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a container is prepared for use.</param>
    [<Extension>]
    static member inline onContainerPrepared(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerPreparedEventArgs -> unit) =
        this.AddScalar(ComponentItemsControl.ContainerPrepared.WithValue(fn))
