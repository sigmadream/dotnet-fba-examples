#!/usr/bin/env -S dotnet fsi
// ============================================================================
// F# Worker — StackExchange.Redis
// ============================================================================
// C# 원본(03-worker.cs)은 BackgroundService + AddRedisClient
// F#은 env ConnectionStrings__cache 사용
// ============================================================================

#r "nuget: StackExchange.Redis, 2.8.16"

open System
open System.Threading
open System.Threading.Tasks
open StackExchange.Redis

let connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__cache")
    |> Option.ofObj
    |> Option.defaultValue "localhost:6379"

let conn = ConnectionMultiplexer.Connect(connectionString)
let database = conn.GetDatabase()

let keyMessage = RedisKey("Message")
let keyLastUpdated = RedisKey("LastUpdated")

// Message 키가 없으면 설정
let ensureMessage () =
    task {
        let! exists = database.KeyExistsAsync(keyMessage)
        if not exists then
            do! database.StringSetAsync(keyMessage, RedisValue("Hello, World!")) :> Task
    }

let runLoop (ct: CancellationToken) =
    let rec loop () =
        async {
            let now = DateTimeOffset.Now
            let value = "Last updated: " + now.ToString()
            do! (database.StringSetAsync(keyLastUpdated, RedisValue(value)) |> Async.AwaitTask) |> Async.Ignore
            printfn "Worker running at: %O" now
            do! Async.Sleep(1000)
            if not ct.IsCancellationRequested then
                return! loop ()
        }
    loop ()

// Message 키 초기화 후 루프
ensureMessage () |> Async.AwaitTask |> Async.RunSynchronously

let run () =
    use cts = new CancellationTokenSource()
    Console.CancelKeyPress.Add(fun _ -> cts.Cancel())
    try
        Async.RunSynchronously(runLoop cts.Token, cancellationToken = cts.Token)
    with :? OperationCanceledException -> ()

run ()
