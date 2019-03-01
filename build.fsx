#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Fsi
nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget FSharp.Core 4.5.0.0 //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators

let paketFile = if Environment.isLinux then "paket" else "paket.exe"
let paketExe = __SOURCE_DIRECTORY__ </> ".paket" </> paketFile

Target.create "Default" (fun _ ->
    Trace.trace "F# Utilities")

Target.create "InstallPaket" (fun _ ->
    if not (File.exists paketExe) then
        DotNet.exec id "tool" "install --tool-path \".paket\" Paket --add-source https://api.nuget.org/v3/index.json"
        |> ignore
    else
        printfn "paket already installed")

Target.create "InstallDependencies" (fun _ ->
    let result =
        CreateProcess.fromRawCommand paketExe ["install"]
        |> Proc.run
    if result.ExitCode <> 0 then failwith "Failed to install dependencies")

Target.create "Test" (fun _ ->
    let (exitCode, messages) = 
        Fsi.exec 
            // profile configuration
            (fun p -> { p with TargetProfile = Fsi.Profile.NetStandard } ) 
            // script to run
            "test.fsx" 
            // script arguments
            []
    match exitCode with
    | 0 -> 
        messages
        |> List.iter Trace.trace
    | _ -> 
        messages
        |> List.iter Trace.traceError
        failwith "Error!")

open Fake.Core.TargetOperators

"InstallPaket"
 ==> "InstallDependencies"

"InstallDependencies"
 ==> "Test"

Target.runOrDefault "Default"
