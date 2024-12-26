﻿using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders;

public partial class DetailsPage : ComponentBase
{
    #region Parameters

    [Parameter] public string Number { get; set; } = string.Empty;

    #endregion

    #region Properties

    public Order Order { get; set; } = null!;

    #endregion

    #region Services

    [Inject] public IOrderHandler Handler { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetOrderByNumberRequest
            {
                Number = Number
            };
            var result = await Handler.GetByNumberAsync(request);

            if (result.IsSuccess)
                Order = result.Data!;
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    #endregion

    #region Methods

    public void RefreshState(Order order)
    {
        Order = order;
        StateHasChanged();
    }

    #endregion
}