using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]

public class OrderProcessController : Controller
{

    private readonly IOrderProcessService _service;
    public OrderProcessController(IOrderProcessService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _service.GetAllAsync();
        return View(orders);
    }





    [HttpGet]
    public async Task<IActionResult> OrderDetails(int id)
    {
        var orderDetails = await _service.GetByIdAsync(id);
        if (orderDetails == null) return NotFound();
        return View(orderDetails);
    }


     

    [HttpGet]
    public async Task<IActionResult> DeleteOrder(int? id)
    {
        if (id == null) return NotFound();
        var order = await _service.GetByIdAsync(id.Value);
        if (order == null) return NotFound();
        return View(order);
    }



  
    [HttpPost]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }



    
    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        await _service.UpdateAsync( id, status);
        return RedirectToAction(nameof(Index));
    }

}
