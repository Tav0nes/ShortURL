using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using ShortURL;
using ShortURL.Models;
using ShortURL.Services;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.tinyurl.com/") });
builder.Services.AddScoped<UrlShortenerService>();

builder.Services.AddScoped<UrlListService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL("Server=localhost;Database=testdb;User=testuser;Password=testpassword;"));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});




await builder.Build().RunAsync();
      