using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using BookShop.Data;
using BookShop.Models;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookShop.Areas.Customer.Controllers;

[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _db;



    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index(int? page, int? categoryId)
    {
        ViewBag.ProductTypes = _db.ProductTypes.ToList();
        ViewBag.CategoryId = categoryId ?? 0;


        if (categoryId != null &&  categoryId != 0)
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Where(c=>c.ProductTypeId==categoryId).ToList().ToPagedList(page ?? 1, 8));
        }
        else
        {
            return View(_db.Products.ToList().ToPagedList(page ?? 1, 8));
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    // Details Action => (Get Method)
    public IActionResult Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var book = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c=>c.Id == id);
        if(book == null)
        {
            return NotFound();
        }
        
        ViewBag.TotalReview = _db.Reviews.Count(c => c.BookId == id);
        if(ViewBag.TotalReview == 0)
        {
            ViewBag.AvgRating = 0;
        }
        else
        {
            ViewBag.AvgRating = _db.Reviews.Where(c => c.BookId == id).Average(r => (double)r.Rating);
        }
        ViewBag.reviews = _db.Reviews.Where(c => c.BookId == id).ToList();
        

        return View(book);
    }



    // Details Action => (POST Method)

    [HttpPost]
    [ActionName("Details")]
    [Authorize]
    public IActionResult BookDetails(int? id)
    {
        List<Products> books = new List<Products>();

        if (id == null)
        {
            return NotFound();
        }
        var book = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        books = HttpContext.Session.Get<List<Products>>("SelectedBooks") ?? new List<Products>();
        books.Add(book);

        HttpContext.Session.Set("SelectedBooks", books);
        return RedirectToAction("Index");
    }

    // Remove Action POST  method

    [HttpPost]
    public IActionResult Remove(int? id)
    {
        List<Products>books = HttpContext.Session.Get<List<Products>>("SelectedBooks");

        var book = books.FirstOrDefault(c => c.Id == id);

        books.Remove(book);
        HttpContext.Session.Set("SelectedBooks", books);

        return RedirectToAction("Index");
    }

    // Get Method, Cart Action

    public IActionResult Cart()
    {
        List<Products> books = HttpContext.Session.Get<List<Products>>("SelectedBooks") ?? new List<Products>();
        return View(books);
    }

    // Get Method , Remove from Cart
    public IActionResult RemoveFromCart(int? id)
    {
        List<Products> books = HttpContext.Session.Get<List<Products>>("SelectedBooks");

        var book = books.FirstOrDefault(c => c.Id == id);

        books.Remove(book);
        HttpContext.Session.Set("SelectedBooks", books);

        return RedirectToAction(nameof(Cart));
    }

    public IActionResult MyOrders()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var orders = _db.Orders
     .Include(o => o.Order_Details)
     .ThenInclude(od => od.Book)
     .Where(o => o.CustomerEmail == userEmail)
     .OrderByDescending(o => o.OrderDate)
     .ToList();

        return View(orders);
    }


    // Add reviews
    [Authorize]
    public IActionResult Review(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var model = new Reviews
        {
            BookId = id ?? 0
        };

        return View(model); 
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id, int rating, string? review)
    {
        var Bookreview = new Reviews
        {
            BookId = id,
            UserEmail = User.FindFirstValue(ClaimTypes.Email),
            Created_at = DateTime.Now,
            Rating = rating,
            Review = review 
        };

        if (!ModelState.IsValid)
        {
            return View("Review", review);
        }

        _db.Reviews.Add(Bookreview);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new{id = id});
    }




}
