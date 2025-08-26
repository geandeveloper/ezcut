using Api.Barbers;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMediator, Mediator>();

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Barber.Api", serviceVersion: "1.0.0"))
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317"); // Jaeger/OTLP
            });
    })
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317"); // Jaeger/OTLP
            });
    });

var app = builder.Build();

app.MapBarberApi();
app.Run();
