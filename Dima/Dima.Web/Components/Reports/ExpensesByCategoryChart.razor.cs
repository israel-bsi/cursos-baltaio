using Dima.Core.Handlers;
using Dima.Core.Request.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports;

public partial class ExpensesByCategoryChartComponent : ComponentBase
{
    #region Properties

    public List<double> Data { get; set; } = [];
    public List<string> Labels { get; set; } = [];

    #endregion

    #region Services

    [Inject] 
    public IReportHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrrides

    protected override async Task OnInitializedAsync()
    {
        await GetExpensesByCategoryAsync();
    }

    private async Task GetExpensesByCategoryAsync()
    {
        var request = new GetExpensesByCategoryRequest();
        var result = await Handler.GetExpensesByCategoryReportAsync(request);
        if (!result.IsSuccess || result.Data is null)
        {
            Snackbar.Add("Falha ao obter dados do relatório", Severity.Error);
            return;
        }

        foreach (var item in result.Data)
        {
            Labels.Add($"{item.Category} ({item.Expenses:C2})");
            Data.Add(-(double)item.Expenses);
        }
    }

    #endregion
}