using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShortURL;
using ShortURL.Services;
using SharedModels.Models;
using System;
using System.Net.Http;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after"); 

// Register HttpClient for TinyURL API
builder.Services.AddHttpClient("TinyUrlApi", client =>
{
    client.BaseAddress = new Uri("https://api.tinyurl.com/");
});

// Register HttpClient for your Web API
builder.Services.AddHttpClient("WebApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:8081");
});


builder.Services.AddSingleton<UrlShortenerService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new UrlShortenerService(httpClientFactory);
});
//builder.Services.AddScoped<UrlListService>();

builder.Services.AddSingleton<UrlListService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new UrlListService(httpClientFactory);
});

builder.Services.AddMudServices();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
    //builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
      