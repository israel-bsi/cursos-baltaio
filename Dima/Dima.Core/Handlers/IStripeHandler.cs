using Dima.Core.Request.Sripe;
using Dima.Core.Response;
using Dima.Core.Response.Stripe;

namespace Dima.Core.Handlers;

public interface IStripeHandler
{
    Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request);
    Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request);
}