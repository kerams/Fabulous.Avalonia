namespace Gallery.Root

open Avalonia
open Avalonia.Controls
open Fabulous
open Gallery
open Gallery.Pages
open Types

module State =
    let mapCmdMsgToCmd cmdMsg =
        match cmdMsg with
        | NewMsg msg -> Cmd.ofMsg msg
        | SubpageCmdMsgs cmdMsgs ->
            let cmd = NavigationState.mapCmdMsgToMsg cmdMsgs
            Cmd.map SubpageMsg cmd

    let init () =
        let model, cmdMsgs = NavigationState.initRoute NavigationRoute.AcrylicPage

        { Navigation = NavigationModel.Init(model)
          IsPanOpen = false
          HeaderText = "AcrylicPage" },
        [ SubpageCmdMsgs cmdMsgs ]

    let update msg model =
        match msg with
        | SubpageMsg subpageMsg ->
            let nav, cmdMsgs = NavigationState.update subpageMsg model.Navigation
            { model with Navigation = nav }, [ SubpageCmdMsgs cmdMsgs ]

        | OpenPanChanged x -> { model with IsPanOpen = x }, []

        | OnSelectionChanged args ->
            let route =
                args.AddedItems
                |> Seq.cast<ListBoxItem>
                |> Seq.tryHead
                |> Option.map(fun x -> unbox<string>(x.Content))

            let routeText =
                match route with
                | Some x -> x
                | None -> failwithf "Could not find route"

            let route = NavigationRoute.GetRoute(routeText)
            let modelRoute, cmdMsgs = NavigationState.initRoute route

            { model with
                Navigation = NavigationModel.Init(modelRoute)
#if MOBILE
                IsPanOpen = not model.IsPanOpen
#endif
                HeaderText = routeText },
            [ SubpageCmdMsgs cmdMsgs ]

        | DoNothing -> model, []

        | Update date ->
            match model.Navigation.CurrentPage with
            | CanvasPageModel _ -> model, [ NewMsg(SubpageMsg(CanvasPageMsg(CanvasPage.Msg.Update(date)))) ]
            | _ -> model, []
