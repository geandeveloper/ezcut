using Api.Barbers;
using Api.Barbers.Create;
using Common.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBarberHandler>());
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseMiddleware<RequestContextMiddleware>();

app.MapBarberApi();
app.Run();
