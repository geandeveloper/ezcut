using Api.Barbers;
using Api.Barbers.Create;
using Common.Observability;
using System.Diagnostics;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CreateBarberHandler>();
builder.Services.AddSingleton<IMediator, Mediator>();

//observability
builder.AddServiceDefaults();
builder.Services.AddSingleton<ITelemetry, Telemetry>();
builder.Services.AddSingleton(new ActivitySource("Api.Barbers"));
builder.Services.AddSingleton(new Meter("Api.Barbers"));
builder.Services.AddOpenTelemetry()
    .WithTracing(tracer =>
    {
        tracer.AddSource("Api.Barbers");
    })
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Api.Barbers");
    });

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapBarberApi();

app.Run();
