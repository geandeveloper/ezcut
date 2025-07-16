using Api.Barbers.Create;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Barbers;

public static class BarberApi
{
    public static IEndpointRouteBuilder MapBarberApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/barbers")
            .WithTags("Barbers");

        group.MapPost("/", CreateBarber)
             .WithName("CreateBarber")
             .Produces<Guid>(StatusCodes.Status201Created)
             .ProducesValidationProblem();

        return app;
    }

    private static async Task<Results<Created<Guid>, BadRequest<string>>> CreateBarber(
        CreateBarberCommand cmd, IMediator mediator, HttpContext httpContext)
    {
        try
        {
            var id = await mediator.Send(cmd);
            return TypedResults.Created($"/barbers/{id}", id);
        }
        catch (ValidationException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
