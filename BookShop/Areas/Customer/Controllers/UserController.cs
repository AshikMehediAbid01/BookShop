using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Customer.Controllers;

[Area("Customer")]


public class UserController : Controller
{
    UserManager<IdentityUser> _userManager;
    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult>Create()
    {
        return View();
    }





}
