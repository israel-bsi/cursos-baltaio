using System.ComponentModel.DataAnnotations;

namespace BlazingShop.Models;

public class Product
{
    [Key]
    [Required(ErrorMessage = "Id é obrigatório")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Título é obrigatório")]
    [MaxLength(50, ErrorMessage = "Título deve ter no máximo 50 caracteres")]
    [MinLength(5, ErrorMessage = "Título deve ter no minimo 5 caracteres")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Preço é obrigatório")]
    [Range(1, 9999, ErrorMessage = "Preço deve ser entre 1 e 9999")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Categoria é obrigatória")]
    [Range(1, 9999, ErrorMessage = "Categoria deve ser entre 1 e 9999")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}