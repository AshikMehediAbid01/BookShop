using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypes {  get; set; } 
        public DbSet<Products> Products {  get; set; } 
        public DbSet<ApplicationUser> ApplicationUsers {  get; set; } 


    }
}
