#!/usr/bin/env -S dotnet fsi
// ============================================================================
// F# Web API — Suave 기반 랜덤 숫자 API
// ============================================================================
// C# 원본(02-random-webapi.cs)은 ASP.NET Core Minimal API를 사용하지만,
// F# .fsx 스크립트에서는 ASP.NET Core의 FrameworkReference를 사용할 수 없으므로
// 경량 웹 프레임워크인 Suave를 사용합니다.
// ============================================================================

#r "nuget: Suave, 2.6.2"
#r "nuget: System.Text.Json, 8.0.0"

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Writers
open System
open System.Text.Json

let rnd = Random()

let app =
    choose [
        path "/" >=> setMimeType "application/json"
                 >=> fun ctx -> async {
            let response =
                {| ts = DateTimeOffset.UtcNow
                   ``val`` = rnd.Next() % 10 |}
            let json = JsonSerializer.Serialize(response)
            return! Successful.OK json ctx
        }
    ]

startWebServer defaultConfig app
