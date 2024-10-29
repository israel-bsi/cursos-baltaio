namespace Dima.Core.Request.Order;

public class PayOrderRequest : Request
{
    public long Id { get; set; }
    public string ExternalReference { get; set; } = string.Empty;
}