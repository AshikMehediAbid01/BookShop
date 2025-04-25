using BookShop.Data;
using BookShop.Models;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _db;

    public OrderController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        return View();
    }


    // Get Method CheckOut Action
    public IActionResult CheckOut()
    {
        return View();
    }


    // Post Method CheckOut Action

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(Order customerOrder)
    {
        List<Products>? books = HttpContext.Session.Get<List<Products>>("SelectedBooks");

        if(books != null)
        {
            foreach(var book in books)
            {
                OrderDetails orderDetails = new OrderDetails();

                orderDetails.BookId = book.Id;

                customerOrder.Order_Details.Add(orderDetails);
            }
        }
        customerOrder.OrderNumber = GetOrderNo();
        _db.Orders.Add(customerOrder);

        await _db.SaveChangesAsync();

        HttpContext.Session.Set("SelectedBooks", new List<Products>());

        
        return View(customerOrder);
    }

    public string GetOrderNo()
    {
        int row = _db.Orders.ToList().Count()+1;
        return row.ToString("000");
    }
}