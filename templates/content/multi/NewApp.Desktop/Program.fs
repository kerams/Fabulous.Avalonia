namespace NewApp.Desktop

open System
open Avalonia
open Avalonia.Themes.Fluent
open Fabulous.Avalonia
open NewApp

module Program =
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [<STAThread; EntryPoint>]
    let Main (args: string array) =

        AppBuilder
            .Configure(fun () -> Program.startApplication App.program)
            .UsePlatformDetect()
            .LogToTrace(?level = None)
            .AfterSetup(fun _ -> FabApplication.Current.AppTheme <- FluentTheme())
            .StartWithClassicDesktopLifetime(args)