open Avalonia
open CrossTray.App

[<EntryPoint>]
let main args =
  AppBuilder
    .Configure<App>()
    .UsePlatformDetect()
    .StartWithClassicDesktopLifetime(args)

