using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]

public class OrderProcessController : Controller
{
    private readonly ApplicationDbContext _db;
    public OrderProcessController(ApplicationDbContext db)
    {
        _db = db;
    }


    // View All Orders
    public IActionResult Index()
    {
        var orders = _db.Orders.ToList();

        return View(orders);
    }




    // View Order Details
    public IActionResult Details(int id)
    {
        var order = _db.Orders
            .Include(o => o.Order_Details)
            .ThenInclude(od => od.Book)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }
           
        return View(order);
    }




    // Delete Order
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var order = await _db.Orders.FirstOrDefaultAsync(m => m.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _db.Orders
            .Include(o => o.Order_Details)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }
            

        _db.OrderDetails.RemoveRange(order.Order_Details);
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Update Order Status
    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        order.Status = status;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
