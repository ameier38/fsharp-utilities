#if !FAKE
#r "netstandard"
#endif
#r @"packages/Expecto/lib/netstandard2.0/Expecto.dll"
#load "src/Env.fs"

open Expecto
open System

let envTests =
    testList "test Env.fs" [
        test "should correctly read environment variable" {
            let expected = "testing"
            Environment.SetEnvironmentVariable("TEST_A", expected)
            match None |> Env.getEnv "TEST_A" with
            | Ok actual ->
                Expect.equal expected actual "should equal expected" 
            | Error err -> failwith err
        }
        test "should fail with no default" {
            Environment.SetEnvironmentVariable("TEST_B", null)
            match None |> Env.getEnv "TEST_B" with
            | Ok actual ->
                failwith "should have failed"
            | Error err ->
                Expect.equal err "error reading env variable TEST_B" "error message should equal"
        }
    ]

runTests defaultConfig envTests |> exit
