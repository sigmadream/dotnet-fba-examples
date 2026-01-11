#!/usr/bin/env dotnet

#:sdk Microsoft.NET.Sdk.BlazorWebAssembly
#:property PublishAot=False
#:property OverrideHtmlAssetPlaceholders=true
#:package Microsoft.AspNetCore.Components.WebAssembly@10.0.1
#:package Microsoft.AspNetCore.Components.WebAssembly.DevServer@10.0.1
#:package MD2RazorGenerator@1.2.2

// Important
// If you change this namespace, you must also change the following parts.
// - app.cs: <namespace> in the using statement
// - _Imports.razor: <namespace> in the @using statement
// - wwwroot/index.html: <namespace>.styles.css reference in the page head tag
// After changing the path, run dotnet run --no-cache app.cs

#:property RootNamespace=app

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using app;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
