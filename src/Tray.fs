namespace CrossTray

open Avalonia.Controls.ApplicationLifetimes
open CrossTray.Core

module Tray =
  let createItem name state callback =
    name
    |> NativeMenuItem.create
    |> NativeMenuItem.setToggleState state
    |> NativeMenuItem.onClick callback

  let init (lifetime: IClassicDesktopStyleApplicationLifetime) =
    let remilia = createItem "remilia" ToggleState.Radio (fun _ -> printfn "れみぃ")
    let flandre = createItem "flandre" ToggleState.Radio (fun _ -> printfn "ふりゃ")
    let patchouli = createItem "patchouli" ToggleState.CheckBox (fun _ -> printfn "ぱちぇ")
    let separator = NativeMenuItem.create "-"
    let exit = createItem "exit" ToggleState.None (fun _ -> lifetime.Shutdown ())
    let items = [ remilia; flandre; patchouli; separator; exit ]

    match "crosstray.assets.yin-yang.ico" |> Embedded.openStreamResult with
    | Ok stream ->
      stream
      |> TrayIcon.create
      |> TrayIcon.setMenu (NativeMenu.create items)
      |> TrayIcon.updateText "sample project"
      |> TrayIcon.onClick (fun _ -> printfn "click")
    | Error ex -> failwith ex

