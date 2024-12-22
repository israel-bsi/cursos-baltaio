using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;

namespace Dima.Api.Endpoints.Orders;

public class GetOrderByNumberEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapGet("/{number}", HandlerAsync)
            .WithName("Orders: Get By Number")
            .WithSummary("Recupera um pedido pelo número")
            .WithDescription("Recupera um pedido pelo número")
            .WithOrder(6)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user, 
        IOrderHandler handler, 
        string number)
    {
        var request = new GetOrderByNumberRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Number = number
        };

        var result = await handler.GetByNumberAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}