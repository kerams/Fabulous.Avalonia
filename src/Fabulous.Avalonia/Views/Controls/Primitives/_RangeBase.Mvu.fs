namespace Fabulous.Avalonia

open Avalonia.Controls.Primitives
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type MvuRangeBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ValueEventData<float, float>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChangedInit: bool

    static member ValueChanged =
        if not MvuRangeBase._ValueChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not MvuRangeBase._ValueChangedInit then
                    MvuRangeBase._ValueChanged <-
                        Attributes.Mvu.defineAvaloniaPropertyWithChangedEvent' "RangeBase_ValueChanged" RangeBase.ValueProperty

                    MvuRangeBase._ValueChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        MvuRangeBase._ValueChanged
