using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models;

public class OrderDetails
{
    public int Id { get; set; }

    [Display(Name = "Order")]
    public int OrderId { get; set; }
    [Display(Name = "Book")]
    public int BookId { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    [ForeignKey("BookId")]
    public Products Book { get; set; }
}
