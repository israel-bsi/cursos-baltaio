namespace Dima.Core.Request.Order;

public class GetOrderByNumberRequest : Request
{
    public string Number { get; set; } = string.Empty;
}