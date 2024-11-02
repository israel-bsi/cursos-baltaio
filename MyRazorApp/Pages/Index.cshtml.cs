using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorApp.Pages;

public class IndexModel : PageModel
{
    public List<Category> Categories { get; set; } = new();
    //public List<Category> Categories { get; set; } =
    //[
    //    new(1, "Beverages", 12.5m),
    //    new(2, "Condiments", 18.5m),
    //    new(3, "Confections", 22.5m),
    //    new(4, "Dairy Products", 28.5m),
    //    new(5, "Grains/Cereals", 32.5m),
    //    new(6, "Meat/Poultry", 36.5m),
    //    new(7, "Produce", 42.5m),
    //    new(8, "Seafood", 48.5m),
    //    new(9, "Vegetables", 52.5m),
    //    new(10, "Fruits", 58.5m),
    //    new(11, "Bread", 62.5m),
    //    new(12, "Pasta", 68.5m),
    //    new(13, "Canned", 72.5m),
    //    new(14, "Frozen", 78.5m),
    //    new(15, "Snacks", 82.5m),
    //    new(16, "Sweets", 88.5m),
    //];
    public async Task OnGet()
    {
        await Task.Delay(5000);
        for (int i = 0; i < 100; i++)
        {
            Categories.Add(new Category(i, $"Categoria {i}", i * 2.12M));
        }
    }

    public void OnPost()
    {
    }
}

public record Category(int Id, string Title, decimal Price);