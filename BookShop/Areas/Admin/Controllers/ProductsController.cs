using System;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _he;

    public ProductsController(ApplicationDbContext db, IWebHostEnvironment he) 
    {
        _db = db;
        _he = he;
    }
    public IActionResult Index(int? page)
    {
        return View(_db.Products.Include(c=>c.ProductTypes).ToList().ToPagedList(page??1,5));
    }

    // POST Index action
    [HttpPost]
    public IActionResult Index(int? lowPrice, int? highPrice)
    {
        var book = _db.Products.Include(c=> c.ProductTypes).Where(c=> c.Price >= lowPrice && c.Price <= highPrice).ToList();

        if(lowPrice == null || highPrice == null)
        {
            book = _db.Products.Include(c => c.ProductTypes).ToList();
        }
        return View(book);
    }


    // GET Method Create Action
    public IActionResult Create()
    {
        ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
        return View();
    }



    [HttpPost]
    // POST Create Action
    public async Task<IActionResult> Create(Products products, IFormFile? Image)
    {

        if (ModelState.IsValid)
        {
            var searchBook = _db.Products.FirstOrDefault(c => c.Name == products.Name);

            if(searchBook != null)
            {
                ViewBag.message = "This book already exists";
                ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                return View(products);
            }

            if (Image != null)// C:\Abid\Project\BookShop\BookShop\BookShop\wwwroot\Images\random.jpg
            {
                var name = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(Image.FileName));
               // await Image.CopyToAsync(new FileStream(name, FileMode.Create));

                using (var stream = new FileStream(name, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                products.Image = "Images/" + Image.FileName;
            }

            else
            {
                products.Image = "Images/NoImageFound.jpg";
            }
                _db.Products.Add(products);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
        return View(products );
    }


    // GET Edit Action

    public IActionResult Edit(int? id)
    {
        ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");  

        if(id == null)
        {
            return NotFound();
        }
        var book = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
        if(book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // Post Edit Action

    [HttpPost]

    public async Task<IActionResult> Edit(Products products, IFormFile? image)
    {
        if (ModelState.IsValid)
        {
            if (image != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(image.FileName));
                //await image.CopyToAsync(new FileStream(name, FileMode.Create));

                using (var stream = new FileStream(name, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                products.Image = "Images/" + image.FileName;
            }
            else
            {
                products.Image = "Images/NoImageFound.jpg";
            }
            _db.Products.Update(products);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
        return View(products);
    }

    // GET Details Action

    public IActionResult Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var book = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);

        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }


    // Get Delete Action

    public IActionResult Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var book = _db.Products.Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();

        if(book == null) {
            return NotFound();
        }



        return View(book);

    }

    // Post Delete Action

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int? id)
    {
        if(id == null)
        {
           return NotFound();
        }

        var book = _db.Products.FirstOrDefault(c => c.Id == id);
        if (book == null) {
            return NotFound();
        }
        _db.Products.Remove(book);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));


    }

}
