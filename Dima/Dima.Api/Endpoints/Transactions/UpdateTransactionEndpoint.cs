﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Transactions;
using Dima.Core.Response;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        UpdateTransactionRequest request,
        long id)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}