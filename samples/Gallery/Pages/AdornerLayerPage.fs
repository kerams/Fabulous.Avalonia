namespace Gallery

open System.Diagnostics
open Avalonia
open Avalonia.Controls
open Avalonia.Layout
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia
open Avalonia.Controls.Primitives

open type Fabulous.Avalonia.View
open Gallery

module AdornerLayerPage =
    type Model = { Angle: float }

    type Msg =
        | ValueChanged of float
        | AddAdorner
        | RemoveAdorner
        | DoNothing

    type CmdMsg = | NoMsg

    let mapCmdMsgToCmd cmdMsg =
        match cmdMsg with
        | NoMsg -> Cmd.none

    let mutable _adorner: Control = null

    let init () = { Angle = 0. }, []

    let adornerButton = ViewRef<Button>()

    let update msg model =
        match msg with
        | ValueChanged value -> { Angle = value }, []
        | AddAdorner ->
            match adornerButton.TryValue with
            | Some adornerButton when _adorner <> null -> AdornerLayer.SetAdorner(adornerButton, _adorner)
            | _ -> ()

            model, []
        | RemoveAdorner ->
            match adornerButton.TryValue with
            | None -> ()
            | Some adornerButton ->
                let adorner = AdornerLayer.GetAdorner(adornerButton)

                if adorner <> null then
                    _adorner <- adorner

                AdornerLayer.SetAdorner(adornerButton, null)

            model, []
        | DoNothing -> model, []

    let program =
        Program.statefulWithCmdMsg init update mapCmdMsgToCmd
        |> Program.withTrace(fun (format, args) -> Debug.WriteLine(format, box args))
        |> Program.withExceptionHandler(fun ex ->
#if DEBUG
            printfn $"Exception: %s{ex.ToString()}"
            false
#else
            true
#endif
        )

    let view () =
        Component(program) {
            let! model = Mvu.State

            Dock() {
                (Grid(coldefs = [ Auto; Star ], rowdefs = [ Auto ]) {
                    TextBlock("Rotation").gridColumn(0).gridRow(0)

                    Slider(0., 360., model.Angle, ValueChanged)
                        .gridColumn(1)
                        .gridRow(0)
                })
                    .dock(Dock.Top)

                (HStack() {
                    Button("Add adorner", AddAdorner).margin(6.)

                    Button("Remove adorner", RemoveAdorner).margin(6.)
                })
                    .dock(Dock.Top)
                    .horizontalAlignment(HorizontalAlignment.Center)

                (Grid(coldefs = [ Pixel(24.); Auto; Pixel(24.) ], rowdefs = [ Pixel(24.); Auto; Pixel(24.) ]) {
                    EmptyBorder()
                        .background(Brushes.Red)
                        .gridColumn(1)
                        .gridRow(0)

                    EmptyBorder()
                        .background(Brushes.Blue)
                        .gridColumn(0)
                        .gridRow(1)

                    EmptyBorder()
                        .background(Brushes.Green)
                        .gridColumn(2)
                        .gridRow(1)

                    EmptyBorder()
                        .background(Brushes.Yellow)
                        .gridColumn(1)
                        .gridRow(2)

                    LayoutTransformControl(
                        Button("Adorner Button", DoNothing)
                            .horizontalAlignment(HorizontalAlignment.Stretch)
                            .horizontalContentAlignment(HorizontalAlignment.Center)
                            .verticalAlignment(VerticalAlignment.Stretch)
                            .verticalContentAlignment(VerticalAlignment.Center)
                            .width(200.)
                            .height(42.)
                            .reference(adornerButton)
                            .adorner(
                                (Canvas() {
                                    Line(Point(-100000, 0), Point(10000, 0))
                                        .stroke(Brushes.Cyan)
                                        .strokeThickness(1.)

                                    Line(Point(-100000, 42), Point(10000, 42))
                                        .stroke(Brushes.Cyan)
                                        .strokeThickness(1.)

                                    Line(Point(0, -100000), Point(0, 10000))
                                        .stroke(Brushes.Cyan)
                                        .strokeThickness(1.)

                                    Line(Point(200, -100000), Point(200, 10000))
                                        .stroke(Brushes.Cyan)
                                        .strokeThickness(1.)
                                })
                                    .horizontalAlignment(HorizontalAlignment.Stretch)
                                    .verticalAlignment(VerticalAlignment.Stretch)
                                    .background(Brushes.Cyan)
                                    .isHitTestVisible(false)
                                    .isClipEnabled(false)
                                    .opacity(0.3)
                                    .isVisible(true)
                            )
                    )
                        .gridColumn(1)
                        .gridRow(1)
                        .layoutTransform(RotateTransform(model.Angle))

                })
                    .verticalAlignment(VerticalAlignment.Center)
                    .horizontalAlignment(HorizontalAlignment.Center)
            }
        }
