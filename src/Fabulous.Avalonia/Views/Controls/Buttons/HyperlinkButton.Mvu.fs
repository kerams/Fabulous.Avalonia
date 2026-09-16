namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuHyperlinkButton =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsVisitedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsVisitedChangedInit: bool

    static member IsVisitedChanged =
        if not MvuHyperlinkButton._IsVisitedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuHyperlinkButton._IsVisitedChangedInit then
                    MvuHyperlinkButton._IsVisitedChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "HyperlinkButton_VisitedChanged" HyperlinkButton.IsVisitedProperty

                    MvuHyperlinkButton._IsVisitedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuHyperlinkButton._IsVisitedChanged

type MvuHyperlinkButtonModifiers =
    /// <summary>Listen to the HyperlinkButton IsVisitedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsVisited value.</param>
    /// <param name="fn">Raised when the IsVisited value changes.</param>
    [<Extension>]
    static member inline onVisitedChanged(this: WidgetBuilder<'msg, #IFabHyperlinkButton>, value: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuHyperlinkButton.IsVisitedChanged.WithValue(ValueEventData.create value fn))
