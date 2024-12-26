﻿using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Request.Sripe;
using Dima.Web.Pages.Orders;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Dima.Web.Components.Orders;

public partial class OrderActionComponent : ComponentBase
{
    #region Parameters

    [CascadingParameter]
    public DetailsPage Parent { get; set; } = null!;

    [Parameter, EditorRequired]
    public Order Order { get; set; } = null!;

    #endregion

    #region Services

    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IOrderHandler OrderHandler { get; set; } = null!;
    [Inject] public IStripeHandler StripeHandler { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Public Methods

    public async void OnCancelButtonClicked()
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            "Deseja realmente cancelar este pedido?",
            yesText: "Sim", noText: "Não");

        if (result is not null && result == true)
            await CancelOrderAsync();
    }

    public async void OnPayButtonClicked()
    {
        await PayOrderAsync();
    }

    public async void OnRefundButtonClicked()
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            "Deseja realmente estornar este pedido?",
            yesText: "Sim", noText: "Não");

        if (result is not null && result == true)
            await RefundOrderAsync();
    }

    #endregion

    #region Private Methods

    private async Task CancelOrderAsync()
    {
        var request = new CancelOrderRequest
        {
            Id = Order.Id
        };

        var result = await OrderHandler.CancelAsync(request);
        if (result.IsSuccess)
        {
            Snackbar.Add(result.Message, Severity.Info);
            Parent.RefreshState(result.Data!);
        }

        else
            Snackbar.Add(result.Message, Severity.Error);
    }

    private async Task PayOrderAsync()
    {
        var request = new CreateSessionRequest
        {
            OrderNumber = Order.Number,
            OrderTotal = (int)Math.Round(Order.Total * 100, 2),
            ProductTitle = Order.Product.Title,
            ProductDescription = Order.Product.Description
        };

        try
        {
            var result = await StripeHandler.CreateSessionAsync(request);
            if (result.IsSuccess == false)
            {
                Snackbar.Add(result.Message, Severity.Error);
                return;
            }
            
            if (result.Data is null)
            {
                Snackbar.Add(result.Message, Severity.Error);
                return;
            }

            await JsRuntime.InvokeVoidAsync("checkout", Configuration.StripePublicKey, result.Data);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    private async Task RefundOrderAsync()
    {
        var request = new RefundOrderRequest
        {
            Id = Order.Id
        };

        var result = await OrderHandler.RefundAsync(request);
        if (result.IsSuccess)
        {
            Snackbar.Add(result.Message, Severity.Info);
            Parent.RefreshState(result.Data!);
        }

        else
            Snackbar.Add(result.Message, Severity.Error);
    }

    #endregion
}