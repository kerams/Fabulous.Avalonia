namespace Fabulous.Avalonia

open Avalonia.Animation

type IFabAnimatable =
    inherit IFabAvaloniaObject

module Animatable =
    let Transitions =
        Attributes.defineAvaloniaListWidgetCollection "Animatable_Transitions" (fun target ->
            let target = (target :?> Animatable)

            if isNull target.Transitions then
                let newColl = Transitions()
                target.Transitions <- newColl
                newColl
            else
                target.Transitions)
