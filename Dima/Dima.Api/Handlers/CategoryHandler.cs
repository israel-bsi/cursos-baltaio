using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Categories;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possivel criar a categoria");
        }
    }
    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = context
                .Categories
                .FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description ?? "";

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
        }
        catch
        {
            //Colocar um código unico de erro pra facilitar a busca
            //return new Response<Category?>(null, 500, "[FP079] Não foi possivel alterar a categoria"); 
            return new Response<Category?>(null, 500, "Não foi possivel alterar a categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = context
                .Categories
                .FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message: "Categoria excluida com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possivel excluir a categoria");
        }
    }
    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null
                ? new Response<Category?>(null, 404, "Categoria não encontrada")
                : new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possivel retornar a categoria");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize) // 1-1 = 0 -> 0 * 25 = 0 depois 2-1 = 1 -> 1 * 25 = 25
                .Take(request.PageSize) //25 itens
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Category>>(null, 500, "Não foi possivel consultar as categorias");
        }
    }
}