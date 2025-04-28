using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models;

public class ProductTypes
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Book Category")]

    public string ProductType { get; set; }
     
}
