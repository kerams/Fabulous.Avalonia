namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls

open Fabulous

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTreeViewItem =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Expanded: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandedInit: bool

    static member Expanded =
        if not MvuTreeViewItem._ExpandedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTreeViewItem._ExpandedInit then
                    MvuTreeViewItem._Expanded <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "TreeViewItem_Expanded" TreeViewItem.IsExpandedProperty

                    MvuTreeViewItem._ExpandedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTreeViewItem._Expanded

type MvuTreeViewItemModifiers =
    /// <summary>Listens to the TreeViewItem Expanded event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">Whether the TreeViewItem is expanded.</param>
    /// <param name="fn">Raised when the Expanded event is fired.</param>
    [<Extension>]
    static member inline onExpandedChanged(this: WidgetBuilder<'msg, #IFabTreeViewItem>, value: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuTreeViewItem.Expanded.WithValue(ValueEventData.create value fn))
