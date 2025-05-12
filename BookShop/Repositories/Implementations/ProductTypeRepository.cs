using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repositories.Implementations;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly ApplicationDbContext _db;
    public ProductTypeRepository(ApplicationDbContext db)
    {
        _db = db;
    }


    // Get All Product Types
    public async Task<IEnumerable<ProductTypes>> GetAllAsync()
    {
        return await _db.ProductTypes.ToListAsync();
    }


    // Get Product Type By Id
    public async Task<ProductTypes?> GetByIdAsync(int id)
    {
        return await _db.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
    }


    // Add Product Type
    public async Task AddAsync(ProductTypes productType)
    {
        await _db.ProductTypes.AddAsync(productType);
        await _db.SaveChangesAsync();
    }

    // Delete Product Type
    public async Task DeleteAsync(ProductTypes productTypes)
    {
        _db.ProductTypes.Remove(productTypes);
        await _db.SaveChangesAsync();
    }


    // Update Product Type
    public async Task UpdateAsync(ProductTypes productType)
    {
        _db.ProductTypes.Update(productType);
        await _db.SaveChangesAsync();
    }
}