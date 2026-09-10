namespace Fabulous.Avalonia

open Avalonia
open Fabulous

[<Sealed>]
type FabulousAppBuilder =
    static member Configure(program: Program<'arg, 'model, 'msg, #IFabApplication>, arg: 'arg, themeFn) =
        AppBuilder.Configure(fun () ->
            FabApplication(
                OnFrameworkInitialized =
                    fun app ->
                        app.Styles.Add (themeFn app)
                        let widget =
                            (View.Component("_") {
                                let! model = Context.Mvu(program.State, arg)
                                program.View model
                            })
                                .Compile()

                        let treeContext: ViewTreeContext =
                            { CanReuseView = Fabulous.Avalonia.ViewHelpers.canReuseView
                              GetViewNode = ViewNode.get
                              GetComponent = Component.get
                              SetComponent = Component.set
                              SyncAction = program.SyncAction
#if DEBUG
                              Logger = program.State.Logger
#endif
                              Dispatch = ignore }

                        let def = WidgetDefinitionStore.get widget.Key
                        let node = def.AttachView(widget, treeContext, ValueNone, app)
                        ignore node
            )
        )