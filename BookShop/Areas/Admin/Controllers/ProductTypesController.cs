using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductTypesController : Controller
{
    private readonly IProductTypeService _service;

    public ProductTypesController(IProductTypeService service )
    {
        _service = service;
    }

    // Get All Product Types
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var bookCategories = await _service.GetAllAsync();
        return View(bookCategories);
    }

    // Create Action get Method
    public ActionResult Create() => View();



    // Create Action Post Method
    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductTypes productTypes)
    {
        if(!ModelState.IsValid) return View(productTypes);

        await _service.CreateAsync(productTypes);
        TempData["alertMsg"] = "Product Type Created Successfully";
        TempData["type"] = "Create";
        
        return RedirectToAction(nameof(Index));
       
    }


    // Edit Action get Method
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var productType = await _service.GetByIdAsync(id.Value);
        if (productType == null) return NotFound();

        return View(productType);
    }


    
    // Edit Action Post  Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductTypes productTypes)
    {
        if (!ModelState.IsValid) return View(productTypes);

        await _service.UpdateAsync(productTypes);
        TempData["alertMsg"] = "Book Category is Updated";
        TempData["type"] = "update";
        
        return RedirectToAction(nameof(Index));             
    }


    // Details Action get  Method
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var productType = await _service.GetByIdAsync(id.Value);
        if (productType == null) return NotFound();
        
        return View(productType);
    }


    // Delete Action get Method
    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null)return NotFound();
        var productType = await _service.GetByIdAsync(id.Value);
        if (productType == null) return NotFound();
        
        return View(productType);
    }


    // Delete Post Action Method
    [HttpPost,ValidateAntiForgeryToken,ActionName("Delete")]
    public async Task<IActionResult> DeleteCategory(int? id) 
    {
        if(id== null) return NotFound();
        var productType =await _service.GetByIdAsync(id.Value);
        if (productType == null)return NotFound();
        await _service.DeleteAsynce(productType);

        TempData["alertMsg"] = "Book Category is deleted";
        TempData["type"] = "delete";

        return RedirectToAction(nameof(Index));

    }

}
