namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuExpander =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Expanded: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Collapsing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CollapsingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Expanding: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandingInit: bool

    static member Expanded =
        if not MvuExpander._ExpandedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuExpander._ExpandedInit then
                    MvuExpander._Expanded <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "Expander_IsExpandedChanged" Expander.IsExpandedProperty

                    MvuExpander._ExpandedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuExpander._Expanded

    static member Collapsing =
        if not MvuExpander._CollapsingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuExpander._CollapsingInit then
                    MvuExpander._Collapsing <-
                        Attributes.Mvu.defineEvent "Expander_Collapsing" (fun target -> (target :?> Expander).Collapsing)

                    MvuExpander._CollapsingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuExpander._Collapsing

    static member Expanding =
        if not MvuExpander._ExpandingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuExpander._ExpandingInit then
                    MvuExpander._Expanding <-
                        Attributes.Mvu.defineEvent "Expander_Expanding" (fun target -> (target :?> Expander).Expanding)

                    MvuExpander._ExpandingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuExpander._Expanding

type MvuExpanderModifiers =
    /// <summary>Listens to the Expander ExpandedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="isExpanded">The IsExpanded value.</param>
    /// <param name="fn">Raised when the ExpandedChanged event fires.</param>
    [<Extension>]
    static member inline onExpandedChanged(this: WidgetBuilder<'msg, #IFabExpander>, isExpanded: bool, fn: bool -> 'msg) =
        this.AddScalar(MvuExpander.Expanded.WithValue(ValueEventData.create isExpanded fn))

    /// <summary>Listens to the Expander Collapsing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Collapsing event fires.</param>
    [<Extension>]
    static member inline onCollapsing(this: WidgetBuilder<'msg, #IFabExpander>, fn: CancelRoutedEventArgs -> 'msg) =
        this.AddScalar(MvuExpander.Collapsing.WithValue(fn))

    /// <summary>Listens to the Expander Expanding event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Expanding event fires.</param>
    [<Extension>]
    static member inline onExpanding(this: WidgetBuilder<'msg, #IFabExpander>, fn: CancelRoutedEventArgs -> 'msg) =
        this.AddScalar(MvuExpander.Expanding.WithValue(fn))
