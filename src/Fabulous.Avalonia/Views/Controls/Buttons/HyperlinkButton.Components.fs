namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentHyperlinkButton =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsVisitedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _IsVisitedChangedInit: bool

    static member IsVisitedChanged =
        if not ComponentHyperlinkButton._IsVisitedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentHyperlinkButton._IsVisitedChangedInit then
                    ComponentHyperlinkButton._IsVisitedChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "HyperlinkButton_VisitedChanged" HyperlinkButton.IsVisitedProperty

                    ComponentHyperlinkButton._IsVisitedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentHyperlinkButton._IsVisitedChanged

type ComponentHyperlinkButtonModifiers =
    /// <summary>Listen to the HyperlinkButton IsVisitedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The IsVisited value.</param>
    /// <param name="fn">Raised when the IsVisited value changes.</param>
    [<Extension>]
    static member inline onVisitedChanged(this: WidgetBuilder<'msg, #IFabHyperlinkButton>, value: bool, fn: bool -> unit) =
        this.AddScalar(ComponentHyperlinkButton.IsVisitedChanged.WithValue(ComponentValueEventData.create value fn))
