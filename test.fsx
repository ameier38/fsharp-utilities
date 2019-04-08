#if !FAKE
#r "netstandard"
#endif
#r @"packages/Expecto/lib/netstandard2.0/Expecto.dll"
#load "src/Env.fs"
#load "src/State.fs"

open Expecto
open Expecto.Flip
open System

module TestEnv =
    let envTests =
        testList "test Env.fs" [
            test "should correctly read environment variable" {
                let expected = "testing"
                Environment.SetEnvironmentVariable("TEST_A", expected)
                match None |> Env.getEnv "TEST_A" with
                | Ok actual ->
                    actual |> Expect.equal "should equal expected" expected 
                | Error err -> failwith err
            }
            test "should fail with no default" {
                Environment.SetEnvironmentVariable("TEST_B", null)
                match None |> Env.getEnv "TEST_B" with
                | Ok actual ->
                    failwith "should have failed"
                | Error err ->
                    err |> Expect.equal "error message should equal" "error reading env variable TEST_B"
            }
        ]

module TestState =
    open State
    type Example =
        { A: string option
          B: int }
    let stateTests =
        testList "test State.fs" [
            test "should correctly update state" {
                let initial = { A = None; B = 0 }
                let getA defaultA = State.S(fun s -> s.A |> Option.defaultValue defaultA, s )
                let putA newA = S(fun s -> (), { s with A = newA })
                let putB newB = S(fun s -> (), { s with B = newB })
                let firstStateS =
                    state {
                        let! newA = getA "a" |> liftS Some
                        do! putA newA
                    }
                firstStateS |> runS initial |> snd |> Expect.equal "first should equal expected" { A = Some "a"; B = 0 }
                let secondStateS =
                    state {
                        let! firstState = firstStateS
                        do! putB 4
                    }
                secondStateS |> runS initial |> snd |> Expect.equal "second should equal expected" { A = Some "a"; B = 4 }
            }
        ]

let tests =
    testList "tests" [
        TestEnv.envTests
        TestState.stateTests
    ]

runTests defaultConfig tests |> exit
