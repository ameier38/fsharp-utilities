#r "paket:
nuget Fake.Net.Http
nuget Fake.DotNet.Fsi
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.Net
open Fake.DotNet
open System.IO

let paketEndpoint = "https://github.com/fsprojects/Paket/releases/download/5.194.4/paket.exe"
let paketExe = Path.Combine(__SOURCE_DIRECTORY__, ".paket", "paket.exe")
let paketCommand = if Environment.isLinux then "mono " + paketExe else paketExe

Target.create "Install" (fun _ ->
    if not (File.Exists paketExe) then
        Trace.trace "downloading Paket"
        Http.downloadFile paketExe paketEndpoint
        |> ignore
    else
        Trace.trace "Paket already exists"
    Trace.trace "Installing dependencies"
    Command.RawCommand(paketExe, Arguments.OfArgs ["install"])
    |> CreateProcess.fromCommand
    |> CreateProcess.withFramework
    |> CreateProcess.ensureExitCode
    |> Proc.run
    |> ignore)
    

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

"Install"
 ==> "Test"

Target.runOrDefault "Test"
