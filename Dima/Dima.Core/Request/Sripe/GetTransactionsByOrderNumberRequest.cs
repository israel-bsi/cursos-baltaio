namespace Dima.Core.Request.Sripe;

public class GetTransactionsByOrderNumberRequest : Request
{
    public string Number { get; set; } = string.Empty;
}