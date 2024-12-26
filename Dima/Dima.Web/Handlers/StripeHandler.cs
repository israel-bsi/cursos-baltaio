using Dima.Core.Handlers;
using Dima.Core.Request.Sripe;
using Dima.Core.Response;
using Dima.Core.Response.Stripe;
using System.Net.Http.Json;

namespace Dima.Web.Handlers;

public class StripeHandler(IHttpClientFactory httpClientFactory) : IStripeHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/payments/stripe/session", request);
        return await result.Content.ReadFromJsonAsync<Response<string?>>()
            ?? new Response<string?>(null, 400, "Falha ao criar a sessão no Stripe");
    }

    public async Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync(
            $"v1/payments/stripe/{request.Number}/transactions", request);

        return await result.Content.ReadFromJsonAsync<Response<List<StripeTransactionResponse>>>()
               ?? new Response<List<StripeTransactionResponse>>(null, 400, "Falha ao consultar transações do pedido");
    }
}