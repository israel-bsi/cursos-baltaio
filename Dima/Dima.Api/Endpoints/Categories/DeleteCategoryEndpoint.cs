using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Categories;
using Dima.Core.Response;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Categories: Delete")
            .WithSummary("Deleta uma categoria")
            .WithDescription("Deleta uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        long id)
    {
        var request = new DeleteCategoryRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };
        var result = await handler.DeleteAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}