namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.LogicalTree
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentStyledElement =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToLogicalTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToLogicalTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromLogicalTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromLogicalTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    static member AttachedToLogicalTree =
        if not ComponentStyledElement._AttachedToLogicalTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentStyledElement._AttachedToLogicalTreeInit then
                    ComponentStyledElement._AttachedToLogicalTree <-
                        Attributes.Component.defineEvent<LogicalTreeAttachmentEventArgs> "StyledElement_AttachedToLogicalTree" (fun target ->
                            (target :?> StyledElement).AttachedToLogicalTree)

                    ComponentStyledElement._AttachedToLogicalTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentStyledElement._AttachedToLogicalTree

    static member DetachedFromLogicalTree =
        if not ComponentStyledElement._DetachedFromLogicalTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentStyledElement._DetachedFromLogicalTreeInit then
                    ComponentStyledElement._DetachedFromLogicalTree <-
                        Attributes.Component.defineEvent<LogicalTreeAttachmentEventArgs> "StyledElement_DetachedFromLogicalTree" (fun target ->
                            (target :?> StyledElement).DetachedFromLogicalTree)

                    ComponentStyledElement._DetachedFromLogicalTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentStyledElement._DetachedFromLogicalTree

    static member ActualThemeVariantChanged =
        if not ComponentStyledElement._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentStyledElement._ActualThemeVariantChangedInit then
                    ComponentStyledElement._ActualThemeVariantChanged <-
                        Attributes.Component.defineEventNoArg "StyledElement_ActualThemeVariantChanged" (fun target -> (target :?> StyledElement).ActualThemeVariantChanged)

                    ComponentStyledElement._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentStyledElement._ActualThemeVariantChanged

type ComponentStyledElementModifiers =
    /// <summary>Listens to the StyledElement AttachedToLogicalTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the styled element is attached to a rooted logical tree.</param>
    [<Extension>]
    static member inline onAttachedToLogicalTree(this: WidgetBuilder<'msg, #IFabStyledElement>, fn: LogicalTreeAttachmentEventArgs -> unit) =
        this.AddScalar(ComponentStyledElement.AttachedToLogicalTree.WithValue(fn))

    /// <summary>Listens to the StyledElement DetachedFromLogicalTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the styled element is detached from a rooted logical tree.</param>
    [<Extension>]
    static member inline onDetachedFromLogicalTree(this: WidgetBuilder<'msg, #IFabStyledElement>, fn: LogicalTreeAttachmentEventArgs -> unit) =
        this.AddScalar(ComponentStyledElement.DetachedFromLogicalTree.WithValue(fn))

    /// <summary>Listens to the StyledElement ActualThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabStyledElement>, msg: unit -> unit) =
        this.AddScalar(ComponentStyledElement.ActualThemeVariantChanged.WithValue(msg))
