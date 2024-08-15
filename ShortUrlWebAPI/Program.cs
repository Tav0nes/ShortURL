using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ShortUrlWebAPI.Data;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Set up logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

    // Use retry logic for database connection
    var retryPolicy = Policy
        .Handle<MySqlConnector.MySqlException>()
        .WaitAndRetry(
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        );

    retryPolicy.Execute(() =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );
});

// Add CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin1", policy =>
    {
        policy.WithOrigins("https://shorturlui.byteurl.duckdns.org")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    options.AddPolicy("AllowSpecificOrigin2", policy =>
    {
        policy.WithOrigins("http://localhost:56608")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    options.AddPolicy("AllowSpecificOrigin3", policy =>
    {
        policy.WithOrigins("http://shorturlui.byteurl.duckdns.org")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services for controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:80");

// Apply the "AllowAll" CORS policy
app.UseCors("AllowAll");

// Middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();  // Comment this out if testing without HTTPS
app.UseAuthorization();

app.MapControllers();

app.Run();

