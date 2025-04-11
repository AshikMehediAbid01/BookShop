using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Image { get; set; }
        [Required]
        public int Price { get; set; }
        [Display(Name="Available")]
        public bool IsAvailabel { get; set; }

        [Display(Name= "Product Type")]
        //[Required]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set;}
    }
}
