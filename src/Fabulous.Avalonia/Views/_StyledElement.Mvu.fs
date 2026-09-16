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
type MvuStyledElement =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToLogicalTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToLogicalTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromLogicalTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.LogicalTree.LogicalTreeAttachmentEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromLogicalTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.MsgValue>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ActualThemeVariantChangedInit: bool

    static member AttachedToLogicalTree =
        if not MvuStyledElement._AttachedToLogicalTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuStyledElement._AttachedToLogicalTreeInit then
                    MvuStyledElement._AttachedToLogicalTree <-
                        Attributes.Mvu.defineEvent<LogicalTreeAttachmentEventArgs> "StyledElement_AttachedToLogicalTree" (fun target ->
                            (target :?> StyledElement).AttachedToLogicalTree)

                    MvuStyledElement._AttachedToLogicalTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuStyledElement._AttachedToLogicalTree

    static member DetachedFromLogicalTree =
        if not MvuStyledElement._DetachedFromLogicalTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuStyledElement._DetachedFromLogicalTreeInit then
                    MvuStyledElement._DetachedFromLogicalTree <-
                        Attributes.Mvu.defineEvent<LogicalTreeAttachmentEventArgs> "StyledElement_DetachedFromLogicalTree" (fun target ->
                            (target :?> StyledElement).DetachedFromLogicalTree)

                    MvuStyledElement._DetachedFromLogicalTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuStyledElement._DetachedFromLogicalTree

    static member ActualThemeVariantChanged =
        if not MvuStyledElement._ActualThemeVariantChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuStyledElement._ActualThemeVariantChangedInit then
                    MvuStyledElement._ActualThemeVariantChanged <-
                        Attributes.Mvu.defineEventNoArg "StyledElement_ActualThemeVariantChanged" (fun target -> (target :?> StyledElement).ActualThemeVariantChanged)

                    MvuStyledElement._ActualThemeVariantChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuStyledElement._ActualThemeVariantChanged

type MvuStyledElementModifiers =
    /// <summary>Listens to the StyledElement AttachedToLogicalTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the styled element is attached to a rooted logical tree.</param>
    [<Extension>]
    static member inline onAttachedToLogicalTree(this: WidgetBuilder<'msg, #IFabStyledElement>, fn: LogicalTreeAttachmentEventArgs -> 'msg) =
        this.AddScalar(MvuStyledElement.AttachedToLogicalTree.WithValue(fn))

    /// <summary>Listens to the StyledElement DetachedFromLogicalTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the styled element is detached from a rooted logical tree.</param>
    [<Extension>]
    static member inline onDetachedFromLogicalTree(this: WidgetBuilder<'msg, #IFabStyledElement>, fn: LogicalTreeAttachmentEventArgs -> 'msg) =
        this.AddScalar(MvuStyledElement.DetachedFromLogicalTree.WithValue(fn))

    /// <summary>Listens to the StyledElement ActualThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the actual theme variant changes.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabStyledElement>, msg: 'msg) =
        this.AddScalar(MvuStyledElement.ActualThemeVariantChanged.WithValue(MsgValue msg))
