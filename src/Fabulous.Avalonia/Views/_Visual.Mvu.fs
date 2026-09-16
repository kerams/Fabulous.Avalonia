namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuVisual =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToVisualTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.VisualTreeAttachmentEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _AttachedToVisualTreeInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromVisualTree: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Avalonia.VisualTreeAttachmentEventArgs -> Fabulous.MsgValue)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _DetachedFromVisualTreeInit: bool

    static member AttachedToVisualTree =
        if not MvuVisual._AttachedToVisualTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuVisual._AttachedToVisualTreeInit then
                    MvuVisual._AttachedToVisualTree <-
                        Attributes.Mvu.defineEvent "VisualAttachedToVisualTree" (fun target -> (target :?> Visual).AttachedToVisualTree)

                    MvuVisual._AttachedToVisualTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuVisual._AttachedToVisualTree

    static member DetachedFromVisualTree =
        if not MvuVisual._DetachedFromVisualTreeInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuVisual._DetachedFromVisualTreeInit then
                    MvuVisual._DetachedFromVisualTree <-
                        Attributes.Mvu.defineEvent "VisualAttachedToVisualTree" (fun target -> (target :?> Visual).DetachedFromVisualTree)

                    MvuVisual._DetachedFromVisualTreeInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuVisual._DetachedFromVisualTree

type MvuVisualModifiers =
    /// <summary>Listens to the Visual AttachedToVisualTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control is attached to a rooted visual tree.</param>
    [<Extension>]
    static member inline onAttachedToVisualTree(this: WidgetBuilder<'msg, #IFabVisual>, fn: VisualTreeAttachmentEventArgs -> 'msg) =
        this.AddScalar(MvuVisual.AttachedToVisualTree.WithValue(fn))

    /// <summary>Listens to the Visual DetachedFromVisualTree event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control is detached from a rooted visual tree.</param>
    [<Extension>]
    static member inline onDetachedFromVisualTree(this: WidgetBuilder<'msg, #IFabVisual>, fn: VisualTreeAttachmentEventArgs -> 'msg) =
        this.AddScalar(MvuVisual.DetachedFromVisualTree.WithValue(fn))

type MvuVisualExtraModifiers =
    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabVisual>, value: Color) =
        VisualModifiers.opacityMask(this, View.SolidColorBrush(value))

    /// <summary>Sets the OpacityMask property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The OpacityMask value.</param>
    [<Extension>]
    static member inline opacityMask(this: WidgetBuilder<'msg, #IFabVisual>, value: string) =
        VisualModifiers.opacityMask(this, View.SolidColorBrush(value))
