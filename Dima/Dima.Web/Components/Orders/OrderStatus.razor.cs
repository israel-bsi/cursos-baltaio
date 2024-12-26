using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Orders;

public partial class OrderStatusComponent : ComponentBase
{
    #region Parameters

    [Parameter, EditorRequired] public EOrderStatus Status { get; set; }

    #endregion
}