module StopLossTests
open Xunit

type Message = 
    | Update of int
    | Sell of int
    | DelayRaise of int
    | Raise
    | DelayFall of int
    | Fall
    | Remove

type Supervisor(delayWorker, sellWorker) =
    let delayWorker = delayWorker
    let sellWorker = sellWorker
    
    let mutable stopLossWorker = fun env msg -> ()

    member this.send message =
        match message with
        | Update _ 
        | Remove 
        | Raise 
        | Fall          -> stopLossWorker this message
        | DelayRaise _
        | DelayFall _   -> delayWorker this message
        | Sell _        -> sellWorker this message

    member this.spawn_stop_loss worker =
        stopLossWorker <- worker


let rec stop_loss_worker actualPrice (env:Supervisor) msg =
    let spawn_me = fun newPrice -> stop_loss_worker newPrice |> env.spawn_stop_loss
    let spawn_empty = fun () -> env.spawn_stop_loss (fun env msg -> ())

    match msg with
    | Update(price) when price > actualPrice -> env.send (DelayRaise 15); spawn_me price
    | Update(price) when price < actualPrice -> env.send (DelayFall 30); spawn_me price
    | Fall
    | Remove        -> env.send (Sell actualPrice); spawn_empty()
    | Raise
    | _             -> ()


let stop_loss_worker_10 = stop_loss_worker 10

[<Fact>]
let ``price doesn't change nothing happens`` () =
    let called = ref false
    let theMock env msg =
        called := true
        
    let env = Supervisor(theMock, theMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 10)

    Assert.False !called

[<Fact>]
let ``price changed from 10 to 11 -> stop loss should wait 15 sek`` () =
    let called = ref false
    let delayWorkerMock env msg = 
        Assert.Equal(DelayRaise(15), msg)
        called := true
    let sellWorkerMock env msg = ()

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 11)

    Assert.True !called

[<Fact>]
let ``price changed from 10 to 9 -> stop loss should wait 30 sek`` () =
    let called = ref false
    let delayWorkerMock env msg = 
        Assert.Equal(DelayFall(30), msg)
        called := true
    let sellWorkerMock env msg = ()

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 9)

    Assert.True !called

[<Fact>]
let ``after 15 sek growth the stop loss should be raised`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(Sell 11, msg)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 11)
    env.send Raise
    env.send Remove

    Assert.True !called

[<Fact>]
let ``after 30 sek fall the stop loss should be sold`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(Sell 9, msg)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 9)
    env.send Fall

    Assert.True !called

[<Fact>]
let ``price 10 change to 11 - earlier as 15 sek change to 9 - after 30 sek should be sold for 9`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 9)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 11)
    env.send (Update 9)
    env.send Raise
    env.send Fall

    Assert.True !called


[<Fact>]
let ``price 10 change to 9 - earlier as 30 sek change to 11 - after 30 sek should be sold for 11`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 11)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 9)
    env.send (Update 11)
    env.send Fall

    Assert.True !called

[<Fact>]
let ``price 10 change to 9 - right away change to 11 - after 30 sek should be sold for 11`` () =
    let called = ref false
    let delayWorkerMock env prices = ()
    let sellWorkerMock env msg = 
        called := true
        Assert.Equal(msg, Sell 11)

    let env = Supervisor(delayWorkerMock, sellWorkerMock)

    env.spawn_stop_loss stop_loss_worker_10
    env.send (Update 9)
    env.send (Update 11)
    env.send Raise
    env.send Fall

    Assert.True !called