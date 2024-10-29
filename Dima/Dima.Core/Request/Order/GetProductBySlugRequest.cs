namespace Dima.Core.Request.Order;

public class GetProductBySlugRequest : Request
{
    public string Slug { get; set; } = string.Empty;
}