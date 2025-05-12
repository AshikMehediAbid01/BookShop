using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace BookShop.Models;

public class Products
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public string Author { get; set; }
    public string? Image { get; set; }
    [Required]
    public int Price { get; set; }
    [Display(Name="Available")]
    public bool IsAvailabel { get; set; }

    [Display(Name= "Product Type")]
    public int ProductTypeId { get; set; }
    [ForeignKey("ProductTypeId")]
    [ValidateNever]
    public virtual ProductTypes ProductTypes { get; set;}


}
