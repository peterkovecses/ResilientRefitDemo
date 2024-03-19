using BookStore.Proxy.Models;
using BookStore.Proxy.Refit;
using Refit;
using Serilog;
using System.Data;
using BookStore.Proxy.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
var httpResilienceOptions = builder.Configuration.GetSection(ConfigKeys.HttpResilience).Get<HttpResilienceOptions>()
                            ?? throw new NoNullAllowedException($"The '{ConfigKeys.HttpResilience}' configuration section appears to be missing or not bound correctly.");

builder.Services
    .AddRefitClient<IBookStoreApiClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7235/"))
    .AddStandardResilienceHandler(options =>
    {
        options.Retry.MaxRetryAttempts = httpResilienceOptions.MaxRetryAttempts;
        options.Retry.Delay = httpResilienceOptions.RetryDelay;
        options.AttemptTimeout.Timeout = httpResilienceOptions.AttemptTimeout;
        options.TotalRequestTimeout.Timeout = httpResilienceOptions.TotalRequestTimeout;
    });

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
