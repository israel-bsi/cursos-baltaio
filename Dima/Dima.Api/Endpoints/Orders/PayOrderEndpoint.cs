﻿using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;

namespace Dima.Api.Endpoints.Orders;

public class PayOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapPost("/{number}/pay", HandleAsync)
            .WithName("Orders: Pay an order")
            .WithSummary("Paga um pedido")
            .WithDescription("Paga um pedido")
            .WithOrder(3)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        string number,
        PayOrderRequest request,
        ClaimsPrincipal user)
    {
        request.Number = number;
        request.UserId = user.Identity?.Name ?? string.Empty;

        var result = await handler.PayAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}