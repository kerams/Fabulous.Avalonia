namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuItemsControl =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerClearing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerClearingEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerClearingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerIndexChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerIndexChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerIndexChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerPrepared: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ContainerPreparedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ContainerPreparedInit: bool

    static member ContainerClearing =
        if not MvuItemsControl._ContainerClearingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuItemsControl._ContainerClearingInit then
                    MvuItemsControl._ContainerClearing <-
                        Attributes.Mvu.defineEvent "ItemsControl_ContainerClearing" (fun target -> (target :?> ItemsControl).ContainerClearing)

                    MvuItemsControl._ContainerClearingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuItemsControl._ContainerClearing

    static member ContainerIndexChanged =
        if not MvuItemsControl._ContainerIndexChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuItemsControl._ContainerIndexChangedInit then
                    MvuItemsControl._ContainerIndexChanged <-
                        Attributes.Mvu.defineEvent "ItemsControl_ContainerIndexChanged" (fun target -> (target :?> ItemsControl).ContainerIndexChanged)

                    MvuItemsControl._ContainerIndexChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuItemsControl._ContainerIndexChanged

    static member ContainerPrepared =
        if not MvuItemsControl._ContainerPreparedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuItemsControl._ContainerPreparedInit then
                    MvuItemsControl._ContainerPrepared <-
                        Attributes.Mvu.defineEvent "ItemsControl_ContainerPrepared" (fun target -> (target :?> ItemsControl).ContainerPrepared)

                    MvuItemsControl._ContainerPreparedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuItemsControl._ContainerPrepared

type MvuItemsControlModifiers =
    /// <summary>Listens to the ItemsControl ContainerClearing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onContainerClearing(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerClearingEventArgs -> 'msg) =
        this.AddScalar(MvuItemsControl.ContainerClearing.WithValue(fn))

    /// <summary>Listens to the ItemsControl ContainerIndexChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the index for the item it represents has changed.</param>
    [<Extension>]
    static member inline onContainerIndexChanged(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerIndexChangedEventArgs -> 'msg) =
        this.AddScalar(MvuItemsControl.ContainerIndexChanged.WithValue(fn))

    /// <summary>Listens to the ItemsControl ContainerPrepared event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a container is prepared for use.</param>
    [<Extension>]
    static member inline onContainerPrepared(this: WidgetBuilder<'msg, #IFabItemsControl>, fn: ContainerPreparedEventArgs -> 'msg) =
        this.AddScalar(MvuItemsControl.ContainerPrepared.WithValue(fn))
