﻿using Dima.Core.Models;
using Dima.Core.Request.Categories;
using Dima.Core.Response;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}