using BookShop.Data;
using Microsoft.AspNetCore.Mvc;
namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

    public DashboardController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        var orders = _db.Orders.ToList();
        ViewBag.Pending = _db.Orders.Count(o => o.Status == "Pending");
        ViewBag.InProcess = _db.Orders.Count(o => o.Status == "InProcess");
        ViewBag.Shipped = _db.Orders.Count(o => o.Status == "Shipped");
        ViewBag.Delivered = _db.Orders.Count(o => o.Status == "Delivered");

        ViewBag.TotalIncome = _db.Orders.Where(st=>st.Status == "Delivered" ).Sum(o => o.TotalPrice);
        ViewBag.ExpectedIncome = _db.Orders.Where(st=>st.Status != "Delivered" ).Sum(o => o.TotalPrice);
        return View(orders);
    }
}
