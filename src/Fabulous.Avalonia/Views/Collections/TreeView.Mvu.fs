namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuTreeView =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.SelectionChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _SelectionChangedInit: bool

    static member SelectionChanged =
        if not MvuTreeView._SelectionChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuTreeView._SelectionChangedInit then
                    MvuTreeView._SelectionChanged <-
                        Attributes.Mvu.defineEvent "TreeView_SelectionChanged" (fun target -> (target :?> TreeView).SelectionChanged)

                    MvuTreeView._SelectionChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuTreeView._SelectionChanged

type MvuTreeViewModifiers =
    /// <summary>Listens to the TreeView SelectionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the TreeView SelectionChanged event is fired.</param>
    [<Extension>]
    static member inline onSelectionChanged(this: WidgetBuilder<'msg, #IFabTreeView>, fn: SelectionChangedEventArgs -> 'msg) =
        this.AddScalar(MvuTreeView.SelectionChanged.WithValue(fn))
