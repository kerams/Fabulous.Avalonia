namespace Fabulous.Avalonia

open Avalonia.Controls.Primitives
open Fabulous.Avalonia

// Values are created on first access instead of in the file's static initializer, which F# runs for every
// top-level value at once. [<DefaultValue>] static fields have no initializer code, so NativeAOT only keeps
// the definitions whose property the app reads.
[<AbstractClass; Sealed>]
type ComponentRangeBase =
    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChanged: Fabulous.ScalarAttributeDefinitions.SimpleScalarAttributeDefinition<Fabulous.Avalonia.ComponentValueEventData<float, float>>

    [<Microsoft.FSharp.Core.DefaultValue>]
    static val mutable private _ValueChangedInit: bool

    static member ValueChanged =
        if not ComponentRangeBase._ValueChangedInit then
            Fabulous.AttributeDefinitionStore.SyncRoot.Enter()

            try
                if not ComponentRangeBase._ValueChangedInit then
                    ComponentRangeBase._ValueChanged <-
                        Attributes.Component.defineAvaloniaPropertyWithChangedEvent' "RangeBase_ValueChanged" RangeBase.ValueProperty

                    ComponentRangeBase._ValueChangedInit <- true
            finally
                Fabulous.AttributeDefinitionStore.SyncRoot.Exit()

        ComponentRangeBase._ValueChanged
