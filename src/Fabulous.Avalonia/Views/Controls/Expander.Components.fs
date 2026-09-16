namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentExpander =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandedChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<bool, bool>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandedChangedInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Collapsing: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _CollapsingInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Expanding: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.Interactivity.CancelRoutedEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ExpandingInit: bool

    static member ExpandedChanged =
        if not ComponentExpander._ExpandedChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentExpander._ExpandedChangedInit then
                    ComponentExpander._ExpandedChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "Expander_IsExpandedChanged" Expander.IsExpandedProperty

                    ComponentExpander._ExpandedChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentExpander._ExpandedChanged

    static member Collapsing =
        if not ComponentExpander._CollapsingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentExpander._CollapsingInit then
                    ComponentExpander._Collapsing <-
                        Attributes.Component.defineEvent "Expander_Collapsing" (fun target -> (target :?> Expander).Collapsing)

                    ComponentExpander._CollapsingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentExpander._Collapsing

    static member Expanding =
        if not ComponentExpander._ExpandingInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentExpander._ExpandingInit then
                    ComponentExpander._Expanding <-
                        Attributes.Component.defineEvent "Expander_Expanding" (fun target -> (target :?> Expander).Expanding)

                    ComponentExpander._ExpandingInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentExpander._Expanding

[<AutoOpen>]
module ComponentExpanderBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Expander widget.</summary>
        /// <param name="header">The header of the expander.</param>
        /// <param name="content">The content of the expander.</param>
        static member Expander(header: string, content: string) =
            let s1 = HeaderedContentControl.HeaderString.WithValue(header)
            let s2 = ContentControl.ContentString.WithValue(content)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabExpander>(
                Expander.WidgetKey,
                &bundle
            )

        /// <summary>Creates a Expander widget.</summary>
        /// <param name="header">The header of the expander.</param>
        /// <param name="content">The content of the expander.</param>
        static member Expander(header: WidgetBuilder<'msg, #IFabControl>, content: string) =
            let bundle = AttributesBundle(StackList.one(ContentControl.ContentString.WithValue(content)),
                    [| HeaderedContentControl.HeaderWidget.WithValue(header.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabExpander>(Expander.WidgetKey, &bundle)

        /// <summary>Creates a Expander widget.</summary>
        /// <param name="header">The header of the expander.</param>
        /// <param name="content">The content of the expander.</param>
        static member Expander(header: string, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.one(HeaderedContentControl.HeaderString.WithValue(header)),
                    [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabExpander>(Expander.WidgetKey, &bundle)

        /// <summary>Creates a Expander widget.</summary>
        /// <param name="header">The header of the expander.</param>
        /// <param name="content">The content of the expander.</param>
        static member Expander(header: WidgetBuilder<'msg, #IFabControl>, content: WidgetBuilder<'msg, #IFabControl>) =
            let bundle = AttributesBundle(StackList.empty(),
                    [| HeaderedContentControl.HeaderWidget.WithValue(header.Compile())
                       ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    [||])
            WidgetBuilder<'msg, IFabExpander>(Expander.WidgetKey, &bundle)

type ComponentExpanderModifiers =
    /// <summary>Listens to the Expander ExpandedChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="isExpanded">The IsExpanded value.</param>
    /// <param name="fn">Raised when the ExpandedChanged event fires.</param>
    [<Extension>]
    static member inline onExpandedChanged(this: WidgetBuilder<'msg, #IFabExpander>, isExpanded: bool, fn: bool -> unit) =
        this.AddScalar(ComponentExpander.ExpandedChanged.WithValue(ComponentValueEventData.create isExpanded fn))

    /// <summary>Listens to the Expander Collapsing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Collapsing event fires.</param>
    [<Extension>]
    static member inline onCollapsing(this: WidgetBuilder<'msg, #IFabExpander>, fn: CancelRoutedEventArgs -> unit) =
        this.AddScalar(ComponentExpander.Collapsing.WithValue(fn))

    /// <summary>Listens to the Expander Expanding event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the Expanding event fires.</param>
    [<Extension>]
    static member inline onExpanding(this: WidgetBuilder<'msg, #IFabExpander>, fn: CancelRoutedEventArgs -> unit) =
        this.AddScalar(ComponentExpander.Expanding.WithValue(fn))
