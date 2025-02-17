namespace CrossTray

module Embedded =
  open System.Reflection

  let assembly = Assembly.GetExecutingAssembly()
  let resources = assembly.GetManifestResourceNames()

  let printAllResources () =
    resources |> Array.iter (fun x -> printfn "%s" x)

  let exists name =
    resources |> Array.contains name

  let openStreamResult name =
    if exists name then
      try
        Ok (assembly.GetManifestResourceStream name)
      with
      | ex -> Error $"Failed to open resource: {ex.Message}"
    else
      Error $"{name} was not found."

