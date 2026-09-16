namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentScrollViewer =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScrollChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ScrollChangedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScrollChangedInit: bool

    static member ScrollChanged =
        if not ComponentScrollViewer._ScrollChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentScrollViewer._ScrollChangedInit then
                    ComponentScrollViewer._ScrollChanged <-
                        Attributes.Component.defineEvent "ScrollViewer_ScrollChangedEvent" (fun target -> (target :?> ScrollViewer).ScrollChanged)

                    ComponentScrollViewer._ScrollChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentScrollViewer._ScrollChanged

type ComponentScrollViewerModifiers =
    /// <summary>Listens to the ScrollViewer ScrollChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the ScrollChanged event fires.</param>
    [<Extension>]
    static member inline onScrollChanged(this: WidgetBuilder<'msg, #IFabScrollViewer>, fn: ScrollChangedEventArgs -> unit) =
        this.AddScalar(ComponentScrollViewer.ScrollChanged.WithValue(fn))
