namespace Fabulous.Avalonia

open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentNativeMenuItem =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _Click: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<(Microsoft.FSharp.Core.Unit -> Microsoft.FSharp.Core.Unit)>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ClickInit: bool

    static member Click =
        if not ComponentNativeMenuItem._ClickInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentNativeMenuItem._ClickInit then
                    ComponentNativeMenuItem._Click <-
                        Attributes.Component.defineEventNoArg "NativeMenuItem_Click" (fun target -> (target :?> NativeMenuItem).Click)

                    ComponentNativeMenuItem._ClickInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentNativeMenuItem._Click

[<AutoOpen>]
module ComponentNativeMenuItemBuilders =
    type Fabulous.Avalonia.View with
        /// <summary>Creates a NativeMenuItem widget.</summary>
        /// <param name="header">The header of the Flyout.</param>
        /// <param name="onClicked">Raised when the menu item is clicked.</param>
        static member NativeMenuItem(header: string, onClicked: unit -> unit) =
            let s1 = NativeMenuItem.Header.WithValue(header)
            let s2 = ComponentNativeMenuItem.Click.WithValue(onClicked)
            let bundle = AttributesBundle(StackList.two(s1, s2), [||], [||])
            WidgetBuilder<'msg, IFabNativeMenuItem>(
                NativeMenuItem.WidgetKey,
                &bundle
            )
