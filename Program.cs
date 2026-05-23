using System.Text.Json.Serialization;
using InBodyApi.Endpoints;
using InBodyApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<IHealthCalculationService, HealthCalculationService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/health", () => Results.Ok(new { success = true, message = "InBody API is running." }));
app.MapHealthEndpoints();

app.MapFallbackToFile("index.html");

app.Run();
