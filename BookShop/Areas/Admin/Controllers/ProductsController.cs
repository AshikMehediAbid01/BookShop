using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using X.PagedList.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _service;

    public ProductsController(IProductService service) 
    {
        _service = service;
    }


    /// <summary>
    /// Show all product
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    /// 
    [HttpGet("products")]
    public async Task<IActionResult> Index(int? page)
    {
        var books = await _service.GetAllAsync();
        var Pagedbooks = books.ToPagedList(page ?? 1, 10);
        return View(Pagedbooks);
    }




    /// <summary>
    ///  Create new product
    /// </summary>
    /// <returns></returns>
    [HttpGet("add-new-product")]
    public async Task<IActionResult> Create()
    {
        ViewData["productTypeId"] = await _service.GetProductTypesSelectListAsync();
        return View();
    }

    // Create Book
    [HttpPost]
    public async Task<IActionResult> Create(Products products, IFormFile? Image)
    {
        if (!ModelState.IsValid)
        {
            ViewData["ProductTypeId"] = await _service.GetProductTypesSelectListAsync();
            return View(products);
        }

        if(_service.IsProductExists(products.Name))
        {
                ViewBag.message = "This book already exists";
                ViewData["ProductTypeId"] = await _service.GetProductTypesSelectListAsync();
                return View(products);
        }

        await _service.CreateAsync(products, Image);
        return RedirectToAction("Index");

    }





    // GET Edit Action
    [HttpGet("update-product/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var book = await _service.GetByIdAsync(id.Value);
        if(book == null)return NotFound();

        ViewData["productTypeId"] = await _service.GetProductTypesSelectListAsync();
        return View(book);
    }




    // Post Edit Action
    [HttpPost]
    public async Task<IActionResult> Edit(Products products, IFormFile? image)
    {
        if (!ModelState.IsValid)
        { 
            ViewData["ProductTypeId"] = await _service.GetProductTypesSelectListAsync();
            return View(products);
        }
        
        await _service.UpdateAsync(products, image);
        return RedirectToAction("Index");
    }




    /// <summary>
    /// This action retrieves the details of a specific book by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("product-details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if(id == null) return NotFound();
        var book = await _service.GetByIdAsync(id.Value);
        if (book == null) return NotFound();
        return View(book);
    }





    /// <summary>
    /// This action retrieves the book to be deleted by its ID and displays a confirmation view.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("delete-product/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)return NotFound();
        var book = await _service.GetByIdAsync(id.Value);
        if(book == null)return NotFound();
        
        return View(book);

    }

    
    // Delete Action post method
    [HttpPost , ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int? id)
    {
        if (id == null) return NotFound();
        await _service.DeleteAsync(id.Value);
        return RedirectToAction(nameof(Index));
    }


}
