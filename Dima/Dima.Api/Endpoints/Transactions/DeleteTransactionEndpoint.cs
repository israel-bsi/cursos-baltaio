using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Transactions;
using Dima.Core.Response;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Deleta uma transação")
            .WithDescription("Deleta uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        long id)
    {
        var request = new DeleteTransactionRequest
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