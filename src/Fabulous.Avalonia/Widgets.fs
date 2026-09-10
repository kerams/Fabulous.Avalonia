namespace Fabulous.Avalonia

open System
open System.Collections
open Avalonia
open Fabulous
open Fabulous.ScalarAttributeDefinitions


type WidgetItems =
    { OriginalItems: IEnumerable
      Template: obj -> Widget }

type WidgetOps<'T when 'T :> AvaloniaObject and 'T: (new: unit -> 'T)> = 'T

module Widgets =
    /// Registers a widget with the given factory function.
    let registerWithFactory<'T when 'T :> AvaloniaObject and 'T: not null> (factory: unit -> 'T) =
        let key = WidgetDefinitionStore.getNextKey()

        let definition =
            { Key = key
              Name = typeof<'T>.Name
              TargetType = typeof<'T>
              CreateView =
                fun (widget, treeContext, parentNode) ->
#if DEBUG
                    treeContext.Logger.Debug("Creating view for {0}", typeof<'T>.Name)
#endif

                    let view = factory()
                    let weakReference = WeakReference(view)

                    let parentNode =
                        match parentNode with
                        | ValueNone -> None
                        | ValueSome node -> Some node

                    let node = new ViewNode(parentNode, &treeContext, weakReference)

                    ViewNode.set node view

                    // additionalSetup view node

                    let mutable prev = ValueNone
                    Reconciler.update treeContext.CanReuseView &prev &widget node

                    struct (node :> IViewNode, box view |> Unchecked.nonNull)
              AttachView =
                fun (widget, treeContext, parentNode, view) ->
#if DEBUG
                    treeContext.Logger.Debug("Attaching view for {0}", typeof<'T>.Name)
#endif

                    let weakReference = WeakReference(view)

                    let parentNode =
                        match parentNode with
                        | ValueNone -> None
                        | ValueSome node -> Some node

                    let node = new ViewNode(parentNode, &treeContext, weakReference)

                    ViewNode.set node view

                    // additionalSetup view node

                    let prev = ValueOption<Widget>.ValueNone
                    Reconciler.update treeContext.CanReuseView &prev &widget node
                    node :> IViewNode }

        WidgetDefinitionStore.set key definition
        key

    /// Registers a widget with the given constructor.
    let register<'T when WidgetOps<'T> and 'T: not null> () = registerWithFactory(fun () -> new 'T())

module WidgetHelpers =
    /// Compiles the templateBuilder into a template.
    let compileTemplate (templateBuilder: 'item -> WidgetBuilder<'msg, 'widget>) item =
        let itm = unbox<'item> item
        (templateBuilder itm).Compile()

    /// Creates a widget with the given key and attributes.
    let inline buildItems<'msg, 'marker, 'itemData, 'itemMarker when 'msg: equality>
        key
        (attrDef: SimpleScalarAttributeDefinition<WidgetItems>)
        (items: seq<'itemData>)
        (itemTemplate: 'itemData -> WidgetBuilder<'msg, 'itemMarker>)
        =
        let data: WidgetItems =
            { OriginalItems = items
              Template = compileTemplate itemTemplate }

        let attrDef = attrDef.WithValue(data)
        WidgetBuilder<'msg, 'marker>(key, &attrDef)

    /// Creates a widget with the given key and attributes.
    let inline buildWidgets<'msg, 'marker when 'msg: equality> (key: WidgetKey) scalars (attrs: WidgetAttribute[]) =
        let mutable atts = struct (scalars, attrs, [||])
        WidgetBuilder<'msg, 'marker>(key, &atts)
