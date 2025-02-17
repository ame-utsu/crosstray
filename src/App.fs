namespace CrossTray

module App =
  open Avalonia
  open Avalonia.Controls
  open Avalonia.Controls.ApplicationLifetimes
  open Avalonia.Themes.Fluent

  type App () =
    inherit Application ()

    override self.OnFrameworkInitializationCompleted () =
      match self.ApplicationLifetime with
      | :? IClassicDesktopStyleApplicationLifetime as lifetime ->
        self.Styles.Add (FluentTheme())
        self.RequestedThemeVariant <-
          if lifetime.Args |> Array.contains "--dark" then Styling.ThemeVariant.Dark else Styling.ThemeVariant.Light

        lifetime.ShutdownMode <- ShutdownMode.OnExplicitShutdown
        Tray.init lifetime |> ignore

        lifetime.Startup.Add(fun _ -> printfn "start")
        lifetime.Exit.Add(fun _ -> printfn "exit")
      | _ -> ()

