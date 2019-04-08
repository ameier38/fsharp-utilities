module State

type S<'Value,'State> = 
    S of ('State -> 'Value * 'State)

let runS state (S f) = f state

let returnS x = S(fun s -> x, s)

let bindS f xS = 
    let run state = 
        let x, newState = runS state xS 
        runS newState (f x)
    S run

let liftS f xS =
    let run state =
        let x, newState = runS state xS
        runS newState (f x |> returnS)
    S run

let getS = S(fun s -> s, s) 

let putS newState = S(fun _ -> (), newState)

let combineS x1S x2S =
    let run state =
        let _, newState = runS state x1S
        runS newState x2S
    S run

let zeroS = S(fun s -> (), s)

let forS s f =
    s 
    |> Seq.map f
    |> Seq.reduceBack combineS


type StateBuilder()=
    member __.Return x = returnS x
    member __.ReturnFrom xS = xS
    member __.Bind (xS, f) = bindS f xS
    member __.Zero () = zeroS
    member __.Combine (x1S, x2S) = combineS x1S x2S
    member __.Delay f : S<'State, 'Value> = f ()
    member __.For (s, f) = forS s f

let state = StateBuilder()
