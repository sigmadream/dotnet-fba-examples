#!/usr/bin/env -S dotnet fsi
// ============================================================================
// F# Minimal API — Suave + StackExchange.Redis
// ============================================================================
// C# 원본(03-minapi.cs)은 ASP.NET Core Minimal API + AddRedisClient
// F#은 Suave + env ConnectionStrings__cache 사용
// ============================================================================

#r "nuget: Suave, 2.6.2"
#r "nuget: StackExchange.Redis, 2.8.16"

open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Writers
open Suave.Successful
open StackExchange.Redis

let connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__cache")
    |> Option.ofObj
    |> Option.defaultValue "localhost:6379"

let conn = ConnectionMultiplexer.Connect(connectionString)
let database = conn.GetDatabase()

let keyMessage = RedisKey("Message")
let keyLastUpdated = RedisKey("LastUpdated")

let getMessage () =
    async {
        let! existsMessage = database.KeyExistsAsync(keyMessage) |> Async.AwaitTask
        if not existsMessage then
            do! (database.StringSetAsync(keyMessage, RedisValue("Hello, World!")) |> Async.AwaitTask) |> Async.Ignore
        let! messageVal = database.StringGetAsync(keyMessage) |> Async.AwaitTask
        let message = messageVal.ToString()
        let! existsLast = database.KeyExistsAsync(keyLastUpdated) |> Async.AwaitTask
        let! fullMessage =
            if existsLast then
                async {
                    let! lastVal = database.StringGetAsync(keyLastUpdated) |> Async.AwaitTask
                    return message + " / " + lastVal.ToString()
                }
            else
                async { return message }
        return fullMessage
    }

let app =
    choose [
        path "/" >=> setMimeType "text/plain"
                 >=> fun ctx ->
            async {
                let! msg = getMessage ()
                return! OK msg ctx
            }
    ]

// Aspire가 WithHttpEndpoint(targetPort, env: "PORT")로 주입. PORT(숫자 또는 URL), ASPNETCORE_URLS, 기본 9080
let parsePort (v: string) =
    if System.String.IsNullOrEmpty(v) then None
    else
        match System.Int32.TryParse(v) with
        | true, n -> Some n
        | _ ->
            let m = System.Text.RegularExpressions.Regex.Match(v, @":(\d+)(?:/|$)")
            if m.Success then Some(int m.Groups.[1].Value) else None

let port =
    parsePort (Environment.GetEnvironmentVariable("PORT"))
    |> Option.orElse (parsePort (Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
    |> Option.defaultValue 9080

let config =
    { defaultConfig with
        bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" port ] }

printfn "MinAPI (F#) listening on http://0.0.0.0:%d/" port
startWebServer config app
