namespace Gallery

open System.Diagnostics
open Avalonia.Controls
open Avalonia.Layout
open Fabulous.Avalonia
open Fabulous

open type Fabulous.Avalonia.View

module EditableNodeView =
    type Node(name, children) =

        let mutable _name: string = name
        let mutable _children: Node list = children

        member this.Name
            with get () = _name
            and set value = _name <- value

        member this.Children
            with get () = _children
            and set value = _children <- value

    type Model = { Node: Node }

    type Msg = NameChanged of string

    let init (node: Node) = { Node = node }

    let update msg (model: Model) =
        match msg with
        | NameChanged name ->
            model.Node.Name <- name
            model

    let program =
        Program.stateful init update
        |> Program.withTrace(fun (format, args) -> Debug.WriteLine(format, box args))
        |> Program.withExceptionHandler(fun ex ->
#if DEBUG
            printfn $"Exception: %s{ex.ToString()}"
            false // unhandled
#else
            true // handled
#endif
        )

    let view node =
        Component(program, node) { TextBox(node.Name, NameChanged) }

module EditableTreeView =

    type Model =
        { Nodes: EditableNodeView.Node list
          Selected: EditableNodeView.Node option }

    type Msg =
        | AddNodeTo of string
        | SelectionItemChanged of SelectionChangedEventArgs

    let branch name (children: EditableNodeView.Node list) = EditableNodeView.Node(name, children)
    let leaf name = branch name []

    let init () =
        let nodes =
            [ branch
                  "Animals"
                  [ branch "Mammals" [ leaf "Lion"; leaf "Cat"; leaf "Zebra" ]
                    branch
                        "Birds"
                        [ leaf "Eagle"
                          leaf "Sparrow"
                          leaf "Dove"
                          leaf "Owl"
                          leaf "Parrot"
                          leaf "Pigeon" ]
                    leaf "Platypus" ]
              branch
                  "Aliens"
                  [ branch "pyramid-building terrestrial" [ leaf "Alpaca"; leaf "Camel"; leaf "Lama" ]
                    branch "extra-terrestrial" [ leaf "Alf"; leaf "E.T."; leaf "Klaatu" ] ] ]

        { Nodes = nodes; Selected = None }, []

    let rec findNodes (predicate: EditableNodeView.Node -> bool) (nodes: EditableNodeView.Node seq) =
        let rec matches (node: EditableNodeView.Node) =
            if predicate node then
                seq { node }
            else
                node.Children |> Seq.collect matches

        nodes |> Seq.collect matches

    let update msg model =
        match msg with
        | AddNodeTo parentNodeName ->
            let newNode = leaf ""

            let updated =
                match findNodes (fun n -> n.Name = parentNodeName) model.Nodes |> Seq.tryExactlyOne with
                | Some node ->
                    node.Children <- node.Children @ [ newNode ]
                    model
                | None ->
                    { model with
                        Nodes = model.Nodes @ [ newNode ] }

            updated, Cmd.none

        | SelectionItemChanged args ->
            let node = args.AddedItems[0] :?> EditableNodeView.Node
            let modelNode = findNodes (fun n -> n = node) model.Nodes |> Seq.tryExactlyOne
            { model with Selected = modelNode }, Cmd.none

    let program =
        Program.statefulWithCmd init update
        |> Program.withTrace(fun (format, args) -> Debug.WriteLine(format, box args))
        |> Program.withExceptionHandler(fun ex ->
#if DEBUG
            printfn $"Exception: %s{ex.ToString()}"
            false
#else
            true
#endif
        )

    /// Functions for leaf nodes for adding a node to a list.
    module AddLeaf =
        let private prefix = "addTo!"

        /// Appends an Add leaf Node (referencing the parentNode or the model if None) to the Node list.
        let appendTo list (parentNode: EditableNodeView.Node option) =
            let parentName = parentNode |> Option.map(_.Name) |> Option.defaultValue ""
            list @ [ leaf(prefix + parentName) ]

        /// Identifies an Add leaf Node.
        let is (node: EditableNodeView.Node) = node.Name.StartsWith(prefix)

        /// Retrieves the parent Node Name from the Node Name of an Add leaf Node.
        let getParentNodeName (node: EditableNodeView.Node) = node.Name.Substring(prefix.Length)

    let view () =
        Component(program) {
            let! model = Mvu.State

            HStack() {
                TreeView(
                    AddLeaf.appendTo model.Nodes None,
                    (fun node ->
                        if AddLeaf.is node then
                            null
                        else
                            AddLeaf.appendTo node.Children (Some node)),
                    (fun node ->
                        HStack(5) {
                            if AddLeaf.is node then
                                Button("+", AddNodeTo(AddLeaf.getParentNodeName node))
                                    .tip(ToolTip("Add a node"))
                            else
                                EditableNodeView
                                    .view(node)
                                    .horizontalAlignment(HorizontalAlignment.Left)
                        })
                )
                    .onSelectionChanged(SelectionItemChanged)

                if model.Selected.IsSome then
                    TextBlock(model.Selected.Value.Name + " selected")
            }
        }
