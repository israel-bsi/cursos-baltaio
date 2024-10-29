using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;

namespace Dima.Api.Endpoints.Orders;

public class CreateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("/", HandleAsync)
            .WithName("Orders: Create order")
            .WithName("Cria um pedido")
            .WithOrder(1)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(IOrderHandler handler, CreateOrderRequest request)
    {
        var result = await handler.CreateAsync(request);

        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest();
    }
}          