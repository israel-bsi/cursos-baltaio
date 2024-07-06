namespace Dima.Core.Request.Transactions;

public class GetTransactionsByPeriodRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}