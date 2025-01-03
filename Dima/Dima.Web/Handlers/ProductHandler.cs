﻿using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;

namespace Dima.Web.Handlers;

public class ProductHandler(IHttpClientFactory httpClientFactory) : IProductHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PagedResponse<List<Product>?>>("/v1/products")
               ?? new PagedResponse<List<Product>?>(null, 400, "Não foi possível obter os produtos");
    }

    public async Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Product?>>($"/v1/products/{request.Slug}")
               ?? new Response<Product?>(null, 400, "Não foi possível obter o produto");
    }
}