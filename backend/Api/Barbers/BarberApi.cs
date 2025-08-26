using Api.Barbers.Create;
using Common.Api.Filters;
using FluentValidation;

namespace Api.Barbers;

public static class BarberApi
{
    public static IEndpointRouteBuilder MapBarberApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/barbers")
            .WithTags("Barbers");

        group.MapPost("/", async (CreateBarberCommand cmd, IMediator mediator) =>
            {
                try
                {
                    var id = await mediator.Send(cmd);
                    return Results.Created($"/barbers/{id}", id);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .AddRequestContextCommandFilter<CreateBarberCommand>()
            .WithName("CreateBarber")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return app;
    }
}
