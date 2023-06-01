namespace Gallery.Pages

open Fabulous.Avalonia
open Fabulous

open type Fabulous.Avalonia.View
open Gallery

module MaskedTextBoxPage =
    type Model = { MaskedText: string }

    type Msg = TextChanged of string

    type CmdMsg = | NoMsg

    let mapCmdMsgToCmd cmdMsg =
        match cmdMsg with
        | NoMsg -> Cmd.none

    let init () = { MaskedText = "" }, []

    let update msg model =
        match msg with
        | TextChanged text -> { model with MaskedText = text }, []

    let view model =
        VStack(spacing = 15) {

            TextBlock("Enter a ten-digit number:")

            MaskedTextBox(model.MaskedText, "(000) 000-0000", TextChanged)

            TextBlock($"You Entered: {model.MaskedText}")
        }
