module DotEnv

open System
open System.IO
open System.Text.RegularExpressions

let getEnv key defaultKeyOpt =
    let failure = sprintf "error reading env variable %s" key |> Error
    try
        match Environment.GetEnvironmentVariable(key) with
        | null -> 
            match defaultKeyOpt with
            | Some k -> k |> Ok
            | None -> failure
        | envVar -> envVar |> Ok
    with
    | _ -> failure

let getSecret secretPath defaultSecretOpt =
    match File.Exists(secretPath) with
    | true ->
        printfn "Found secret file at %s" secretPath
        File.ReadAllText(secretPath) |> Ok
    | false ->
        match defaultSecretOpt with
        | Some secret -> secret |> Ok
        | None -> 
            sprintf "could not find secret file at %s and no default provided" secretPath
            |> Error

/// matches VARIABLE=value
/// lines starting with # are ignored
let envPattern = @"^(?!#)([A-Z_]+)=(.+)$"

let (|Env|_|) line =
    let m = Regex.Match(line, envPattern)
    match m.Success with
    | true -> Some(List.tail [ for g in m.Groups -> g.Value ])
    | false -> None

let setEnvFromLine line =
    match line with
    | Env [key; value] -> Environment.SetEnvironmentVariable(key, value)
    | _ -> ()

let loadDotEnv dotEnvPath = 
    match File.Exists(dotEnvPath) with
    | true ->
        printfn "Found environment file at %s" dotEnvPath
        File.ReadLines(dotEnvPath)
        |> Seq.iter setEnvFromLine
    | false -> printfn "Could not find environment file at %s" dotEnvPath
