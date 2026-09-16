namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Avalonia.Interactivity
open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentTreeViewItem =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Expanded: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandedInit: bool

    static member Expanded =
        if not ComponentTreeViewItem._ExpandedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentTreeViewItem._ExpandedInit then
                    ComponentTreeViewItem._Expanded <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "TreeViewItem_Expanded" TreeViewItem.IsExpandedProperty

                    ComponentTreeViewItem._ExpandedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentTreeViewItem._Expanded

type ComponentTreeViewItemModifiers =
    /// <summary>Listens to the TreeViewItem Expanded event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Whether the TreeViewItem is expanded.</param>
    /// <param name="fn">Raised when the Expanded event is fired.</param>
    [<Extension>]
    static member inline onExpandedChanged(this: WidgetBuilder<'msg, #IFabTreeViewItem>, value: bool, fn: bool -> unit) =
        this.AddScalar(ComponentTreeViewItem.Expanded.WithValue(ComponentValueEventData.create value fn))
