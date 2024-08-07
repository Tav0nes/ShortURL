using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using ShortURL;
using ShortURL.Services;
using System;
using SharedModels.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for TinyURL API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.tinyurl.com/") });

// Register HttpClient for your Web API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:8080") });

builder.Services.AddScoped<UrlShortenerService>();
builder.Services.AddScoped<UrlListService>();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
      