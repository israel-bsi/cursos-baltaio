using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Orders;

public class GetAllProductsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapGet("/", HandlerAsync)
            .WithName("Products: Get All")
            .WithSummary("Recupera todos os produtos")
            .WithDescription("Recupera todos os produtos")
            .WithOrder(1)
            .Produces<PagedResponse<List<Product>?>>();

    private static async Task<IResult> HandlerAsync(
        IProductHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllProductsRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}