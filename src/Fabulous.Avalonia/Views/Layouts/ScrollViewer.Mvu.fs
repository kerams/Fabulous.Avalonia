namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuScrollViewer =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScrollChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Controls.ScrollChangedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ScrollChangedInit: bool

    static member ScrollChanged =
        if not MvuScrollViewer._ScrollChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuScrollViewer._ScrollChangedInit then
                    MvuScrollViewer._ScrollChanged <-
                        Attributes.Mvu.defineEvent "ScrollViewer_ScrollChangedEvent" (fun target -> (target :?> ScrollViewer).ScrollChanged)

                    MvuScrollViewer._ScrollChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuScrollViewer._ScrollChanged

type MvuScrollViewerModifiers =
    /// <summary>Listens to the ScrollViewer ScrollChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the ScrollChanged event fires.</param>
    [<Extension>]
    static member inline onScrollChanged(this: WidgetBuilder<'msg, #IFabScrollViewer>, fn: ScrollChangedEventArgs -> 'msg) =
        this.AddScalar(MvuScrollViewer.ScrollChanged.WithValue(fn))
