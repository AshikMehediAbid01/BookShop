using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repositories.Implementations;

public class ProductTypeRepository : Repository<ProductTypes>, IProductTypeRepository
{
    public ProductTypeRepository(ApplicationDbContext db) : base(db)
    {
    }

}