using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Transactions;
using Dima.Core.Response;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Transactions: Get by Id")
            .WithSummary("Recupera uma transação")
            .WithDescription("Recupera uma transação")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };
        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}