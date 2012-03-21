module StopLossTests
open Xunit

type Message = 
    | UpdatePrice of int
    | DelayRaise
    | DoRaise
    | DelayFall
    | DoFall
    | Sell of int
    | RemoveStopLoss

type Supervisor(delayWorker, sellWorker) =
    let delayWorker = delayWorker
    let sellWorker = sellWorker
    
    let mutable stopLossWorker = fun env msg -> ()

    member this.send message =
        match message with
        | UpdatePrice _ 
        | DoRaise 
        | DoFall
        | RemoveStopLoss -> stopLossWorker this message

        | DelayRaise _
        | DelayFall _   -> delayWorker this message

        | Sell _        -> sellWorker this message

    member this.spawn_stop_loss worker =
        stopLossWorker <- worker


let rec stop_loss_worker actualPrice (env:Supervisor) msg =
    let spawn_me = fun newPrice -> stop_loss_worker newPrice |> env.spawn_stop_loss
    let spawn_empty = fun () -> env.spawn_stop_loss (fun env msg -> ())

    match msg with
    | UpdatePrice(newPrice) when newPrice > actualPrice -> env.send DelayRaise; spawn_me newPrice
    | UpdatePrice(newPrice) when newPrice < actualPrice -> env.send DelayFall; spawn_me newPrice
    | DoFall
    | RemoveStopLoss        -> env.send (Sell actualPrice); spawn_empty()
    | DoRaise
    | _             -> ()


let worker_10 = stop_loss_worker 10

[<Fact>]
let ``price doesn't change -> nothing happens`` () =
    let called = ref false
    let theMock env msg =
        called := true
        
    let env = Supervisor(theMock, theMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 10)

    Assert.False !called

[<Fact>]
let ``price changed from 10 to 11 -> stop loss has raise delay`` () =
    let called = ref false
    let delayWorkerMock env msg = 
        Assert.Equal(DelayRaise, msg)
        called := true
    let sellWorkerMock env msg = ()

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 11)

    Assert.True !called

[<Fact>]
let ``price changed from 10 to 9 -> stop loss has fall delay`` () =
    let called = ref false
    let delayWorkerMock env msg = 
        Assert.Equal(DelayFall, msg)
        called := true
    let sellWorkerMock env msg = ()

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 9)

    Assert.True !called

[<Fact>]
let ``after raise delay the stop loss should be raised`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(Sell 11, msg)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 11)
    env.send DoRaise

    Assert.False !called

    env.send RemoveStopLoss

    Assert.True !called

[<Fact>]
let ``after fall delay the stop loss should be sold`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(Sell 9, msg)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 9)
    env.send DoFall

    Assert.True !called

[<Fact>]
let ``price 10 change to 11 with raise delay then to 9 with long fall delay -> should be sold for 9`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 9)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 11)
    env.send (UpdatePrice 9)
    env.send DoRaise
    env.send DoFall

    Assert.True !called


[<Fact>]
let ``price 10 change to 9 with long fall delay then to 11 -> should be sold for 11`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 11)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 9)
    env.send (UpdatePrice 11)
    env.send DoFall

    Assert.True !called

[<Fact>]
let ``price 10 change to 9 right away change to 11 - after long fall delay should be sold for 11`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 11)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss worker_10
    env.send (UpdatePrice 9)
    env.send (UpdatePrice 11)
    env.send DoRaise
    env.send DoFall

    Assert.True !called