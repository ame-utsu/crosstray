namespace CrossTray

module Core =
  open Avalonia.Controls
  open Avalonia.Media.Imaging
  open System.IO

  type ToggleState =
    | None
    | CheckBox
    | Radio

  module NativeMenuItem =
    let create header =
      let item = new NativeMenuItem ()
      item.Header <- header
      item

    let checkItem (item: NativeMenuItem) =
      item.IsChecked <- true
      item

    let uncheckItem (item: NativeMenuItem) =
      item.IsChecked <- false
      item

    let setSubmenu menu (item: NativeMenuItem) =
      item.Menu <- menu
      item

    let onClick (callback: unit -> unit) (item: NativeMenuItem) =
      item.Click.Add (fun _ -> callback ())
      item

    let onChecked (callback: unit -> unit) (item: NativeMenuItem) =
      item.PropertyChanged.Add (fun e ->
        if e.Property.Name = "IsChecked" && item.IsChecked then callback ()
      )
      item

    let onUnchecked (callback: unit -> unit) (item: NativeMenuItem) =
      item.PropertyChanged.Add (fun e ->
        if e.Property.Name = "IsChecked" && not item.IsChecked then callback ()
      )
      item

    let setToggleState state (item: NativeMenuItem) =
      match state with
      | None -> item.ToggleType <- NativeMenuItemToggleType.None
      | CheckBox -> item.ToggleType <- NativeMenuItemToggleType.CheckBox
      | Radio -> item.ToggleType <- NativeMenuItemToggleType.Radio
      item

  module NativeMenu =
    let create (items: NativeMenuItem list) =
      let menu = new NativeMenu ()
      items |> List.iter menu.Add
      menu

  module TrayIcon =
    let create (icon: obj) =
      if icon = null then failwith "Icon cannot be null."
      let trayIcon = new TrayIcon ()
      match icon with
      | :? Bitmap as bitmap -> trayIcon.Icon <- WindowIcon bitmap
      | :? Stream as stream ->
        if not (stream.CanRead || stream.Length > 0L) then
          failwith "Stream is invalid or empty."
        trayIcon.Icon <- WindowIcon stream
      | :? string as fileName ->
        if not (Path.Exists (fileName)) then
          failwith $"'{fileName}' was not found."
        trayIcon.Icon <- WindowIcon fileName
      | _ -> failwith "Unsupported icon type."
      trayIcon

    let updateText hint (trayIcon: TrayIcon) =
      trayIcon.ToolTipText <- hint
      trayIcon

    let setMenu menu (trayIcon: TrayIcon) =
      trayIcon.Menu <- menu
      trayIcon

    let showIcon (trayIcon: TrayIcon) =
      trayIcon.IsVisible <- true
      trayIcon

    let hideIcon (trayIcon: TrayIcon) =
      trayIcon.IsVisible <- false
      trayIcon

    let onClick (callback: unit -> unit) (trayIcon: TrayIcon) =
      trayIcon.Clicked.Add (fun _ -> callback ())
      trayIcon

