using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductTypesController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductTypesController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var data = _db.ProductTypes.ToList();
        return View(data);
    }

    // Create Method get Action 
    public ActionResult Create()
    {
        return View();
    }

    // Create Method Post Action 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductTypes productTypes)
    {
        if(ModelState.IsValid)
        {
            _db.ProductTypes.Add(productTypes);
            await _db.SaveChangesAsync();
            TempData["alertMsg"] = "Product Type Created Successfully";
            TempData["type"] = "Create";
            return RedirectToAction(nameof(Index));
        }
        return View(productTypes); 
    }

    // Edit Action get  Method
    public IActionResult Edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var productType = _db.ProductTypes.Find(id);
        if(productType == null)
        {
            return NotFound();
        }
        return View(productType);
    }
    
    // Edit Action Post  Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductTypes productTypes)
    {
        if (ModelState.IsValid)
        {
            _db.Update(productTypes);
            await _db.SaveChangesAsync();
            TempData["alertMsg"] = "Book Type is Updated";
            TempData["type"] = "update";
            return RedirectToAction(nameof(Index));
        }
     
        return View(productTypes);
    }


    // Details Action get  Method
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var productType = _db.ProductTypes.Find(id);
        if (productType == null)
        {
            return NotFound();
        }
        return View(productType);
    }

/*    // Details Action Post  Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Details(ProductTypes productTypes)
    {
        return RedirectToAction(nameof(Index));
    }*/

    // Delete get Action Method
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var productType = _db.ProductTypes.Find(id);
        if (productType == null)
        {
            return NotFound();
        }
        return View(productType);
    }

    // Delete Post Action Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id,ProductTypes productTypes)
    {
        Console.WriteLine("########################"+productTypes.Id);
        if(id== null)
        {
            return NotFound();
        }

        if(id != productTypes.Id)
        {
            return NotFound();
        }
        var productType = _db.ProductTypes.Find(id);
        if (productType == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)  
        {
            _db.Remove(productType);
            await _db.SaveChangesAsync();
            TempData["alertMsg"] = "Book Type is deleted";
            TempData["type"] = "delete";
            return RedirectToAction(nameof(Index));
        }

        return View(productTypes);
    }

}
