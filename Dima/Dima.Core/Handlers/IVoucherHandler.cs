using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;

namespace Dima.Core.Handlers;

public interface IVoucherHandler
{

    Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request);
}