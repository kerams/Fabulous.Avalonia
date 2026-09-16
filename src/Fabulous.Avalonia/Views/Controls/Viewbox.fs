namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabViewBox =
    inherit IFabControl

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ViewBox =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WidgetKey: int

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _WidgetKeyInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Stretch: Fabulous.ScalarAttributeDefinitions.SmallScalarAttributeDefinition<Avalonia.Media.Stretch>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _StretchInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _StretchDirection: Fabulous.ScalarAttributeDefinitions.SmallScalarAttributeDefinition<Avalonia.Media.StretchDirection>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _StretchDirectionInit: bool

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Child: Fabulous.WidgetAttributeDefinitions.WidgetAttributeDefinition

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ChildInit: bool

    static member WidgetKey =
        if not ViewBox._WidgetKeyInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ViewBox._WidgetKeyInit then
                    ViewBox._WidgetKey <-
                        Widgets.register<Viewbox>()

                    ViewBox._WidgetKeyInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ViewBox._WidgetKey

    static member Stretch =
        if not ViewBox._StretchInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ViewBox._StretchInit then
                    ViewBox._Stretch <-
                        Attributes.defineAvaloniaPropertyEnum Viewbox.StretchProperty

                    ViewBox._StretchInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ViewBox._Stretch

    static member StretchDirection =
        if not ViewBox._StretchDirectionInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ViewBox._StretchDirectionInit then
                    ViewBox._StretchDirection <-
                        Attributes.defineAvaloniaPropertyEnum Viewbox.StretchDirectionProperty

                    ViewBox._StretchDirectionInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ViewBox._StretchDirection

    static member Child =
        if not ViewBox._ChildInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ViewBox._ChildInit then
                    ViewBox._Child <-
                        Attributes.defineAvaloniaPropertyWidget Viewbox.ChildProperty

                    ViewBox._ChildInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ViewBox._Child

[<AutoOpen>]
module ViewBoxBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ViewBox widget.</summary>
        /// <param name="content">The content of the ViewBox.</param>
        static member ViewBox(content: WidgetBuilder<'msg, #IFabControl>) =
            let widget = ViewBox.Child.WithValue(content.Compile())
            WidgetBuilder<'msg, IFabViewBox>(ViewBox.WidgetKey, &widget)

type ViewBoxModifiers =
    /// <summary>Sets the Stretch property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Stretch value.</param>
    [<Extension>]
    static member inline stretch(this: WidgetBuilder<'msg, #IFabViewBox>, value: Stretch) =
        this.AddScalar(ViewBox.Stretch.WithValue(value))

    /// <summary>Sets the StretchDirection property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The StretchDirection value.</param>
    [<Extension>]
    static member inline stretchDirection(this: WidgetBuilder<'msg, #IFabViewBox>, value: StretchDirection) =
        this.AddScalar(ViewBox.StretchDirection.WithValue(value))

    /// <summary>Link a ViewRef to access the direct ViewBox control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabViewBox>, value: ViewRef<Viewbox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
