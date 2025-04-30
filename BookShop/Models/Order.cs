using System.ComponentModel.DataAnnotations;

namespace BookShop.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    [Required]
    public string CustomerName { get; set; }
    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; }
    [Required]
    public string CustomerPhone { get; set; }
    [Required]
    [Display(Name = "Address")]
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string? Note { get; set; } 
    public int TotalPrice { get; set; }
    public virtual List<OrderDetails> Order_Details { get; set; } = new List<OrderDetails>();
}
