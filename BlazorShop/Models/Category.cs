using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o título")]
    [MinLength(3, ErrorMessage = "A categoria deve ter pelo menos 3 caracteres")]
    [MaxLength(120, ErrorMessage = "A categoria deve ter no máximo 120 caracteres")]
    public string Title { get; set; } = string.Empty;
}