﻿using Dima.Core.Request.Categories;
using Dima.Web.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public CreateCategoryRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public CategoryHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject] 
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    public async Task OnValidSubmit()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch(Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}