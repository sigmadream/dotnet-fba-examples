#!/usr/bin/env dotnet
// ============================================================================
// C# App Host — Aspire AppHost.Sdk + Garnet 상세 설명
// ============================================================================
// 이 코드는 Microsoft Aspire(AppHost.Sdk)를 기반으로 분산 애플리케이션 호스트를 구성하는 C# AppHost 정의
// - Aspire는 .NET 8에 도입된 분산 애플리케이션 개발 플랫폼 
//    - 다양한 서비스와 앱을 손쉽게 오케스트레이션하고 로컬 개발 환경을 자동화하는 기능 제공
//    - AppHost는 Aspire 솔루션의 중심이 되는 구성 엔트리포인트
//    - 여러 프로젝트(API, 백그라운드 서비스, 캐시 등)를 개별적으로 또는 함께 실행 및 관리해줌
// - AppHost는 F#을 기본적으로 지원하지 않으므로 C#으로 작성
//    - Aspire의 AppHost는 현재 F# 진입점(`Program.fs` 등 F# 프로젝트)으로 직접 실행할 수 없음
//    - 따라서 AppHost 자체는 C#으로 구현
//    - 그러나 F# 스크립트(.fsx) 파일 실행이 필요할 경우 `AddExecutable` 메서드를 이용해 외부 실행파일
// ============================================================================

#:sdk Aspire.AppHost.Sdk@13.0.0
#:property PublishAot=false
#:package Aspire.Hosting.Garnet@13.0.0

#pragma warning disable ASPIRECSHARPAPPS001

using Microsoft.Extensions.Configuration;

// To specify password/secrets:
// dotnet user-secrets set --id=apphosttest ConfigName Value

var builder = DistributedApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("03-apphost.json")
    .AddUserSecrets("apphosttest")
    .AddEnvironmentVariables();

var garnet = builder.AddGarnet("cache");

// 인자에 "fsharp"이 있으면 F#(dotnet fsi), 없으면 기본값 C# 실행
var useFSharp = args.Contains("fsharp");

if (useFSharp)
{
    _ = builder.AddExecutable("worker", "dotnet", ".", "fsi", "03-worker.fsx")
        .WithReference(garnet)
        .WaitFor(garnet);

    _ = builder.AddExecutable("minapi", "dotnet", ".", "fsi", "03-minapi.fsx")
        .WithReference(garnet)
        .WaitFor(garnet)
        .WithHttpEndpoint(targetPort: 9080, env: "PORT");
}
else
{
    _ = builder.AddCSharpApp("worker", "03-worker.cs")
        .WithReference(garnet)
        .WaitFor(garnet);

    _ = builder.AddCSharpApp("minapi", "03-minapi.cs")
        .WithReference(garnet)
        .WaitFor(garnet)
        .WithHttpsEndpoint()
        .WithHttpEndpoint();
}

using var app = builder.Build();
app.Run();
