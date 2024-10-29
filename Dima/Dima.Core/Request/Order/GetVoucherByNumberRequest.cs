namespace Dima.Core.Request.Order;

public class GetVoucherByNumberRequest : Request
{ 
    public string Number { get; set; } = string.Empty;
}