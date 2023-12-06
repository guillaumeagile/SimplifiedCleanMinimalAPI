namespace CleanMinimalApi.WebAPI.Endpoints;


using Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RequestsDTO;
using Validators;
using ViewsDTO;

public static class ReviewsEndpoints
{
    public static WebApplication MapReviewEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/reviews")
            .WithTags("reviews")
            .WithOpenApi();

        _ = root.MapGet("/", GetReviews)
           // .Produces<List<Entities.Review>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Reviews")
            .WithDescription("\n    GET /Reviews");

        _ = root.MapGet("/{id:guid}", GetReviewById)
            .AddEndpointFilter<ValidationFilter<Guid>>()
           // .Produces<Entities.Review>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Lookup a Review by its Ids")
            .WithDescription("\n    GET /Reviews/00000000-0000-0000-0000-000000000000");

        _ = root.MapPost("/", CreateReview)
            .AddEndpointFilter<ValidationFilter<CreateReviewRequest>>()
            //.Produces<Entities.Review>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Create a Review")
            .WithDescription("\n    POST /Reviews\n     {         \"authorId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"movieId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"stars\": 5       }");

        _ = root.MapDelete("/{id:guid}", DeleteReview)
            .AddEndpointFilter<ValidationFilter<Guid>>()
           // .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Delete a Review by its Id")
            .WithDescription("\n    DELETE /Reviews/00000000-0000-0000-0000-000000000000");

        _ = root.MapPut("/{id:guid}", UpdateReview)
            .AddEndpointFilter<ValidationFilter<Guid>>()
            .AddEndpointFilter<ValidationFilter<UpdateReviewRequest>>()
           // .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Update a Review")
            .WithDescription("\n    PUT /Reviews/00000000-0000-0000-0000-000000000000\n     {         \"authorId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"movieId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"stars\": 5       }");

        return app;
    }

    public static async Task<IResult> GetReviews(IMediator mediator)
    {
        try
        {
            return Results.Ok(
                //await mediator.Send(new Queries.GetReviews.GetReviewsQuery())
                // instead of using mediator (if we don"t want it), call directly
                );
        }
        catch (Exception ex)
        {
            // adapt this part with Either.Left
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetReviewById([FromRoute] Guid id, IMediator mediator)
    {
        try
        {
            return Results.Ok(
                // here, there is a dependency to the Queries as defined in the App/Core, so IT IS NOT decoupled as it should
           /*     await mediator.Send(new Queries.GetReviewById.GetReviewByIdQuery
            {
                Id = id
            })
            */
           );
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> CreateReview(
        [FromBody] CreateReviewRequest request,
        IMediator mediator,
        HttpRequest httpRequest)
    {
        try
        {
            return Results.Created(
                UriHelper.GetEncodedUrl(httpRequest),
                new Review(stars: 0)


                );
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> UpdateReview(
        [FromRoute] Guid id,
        [FromBody] UpdateReviewRequest bodyRequest,
        IMediator mediator)
    {
        try
        {
          /*  _ = await mediator.Send(new Commands.UpdateReview.UpdateReviewCommand
            {
                Id = id,
                AuthorId = bodyRequest.AuthorId,
                MovieId = bodyRequest.MovieId,
                Stars = bodyRequest.Stars
            });*/

            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> DeleteReview([FromRoute] Guid id, IMediator mediator)
    {
        try
        {
            /*_ = await mediator.Send(new Commands.DeleteReview.DeleteReviewCommand
            {
                Id = id,
            });
*/
            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}
