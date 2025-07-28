using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Products>> GetAllAsync() 
    {
        return await _db.Products.Include(c => c.ProductTypes).AsNoTracking().ToListAsync();
    }

    public async Task<Products?> GetByIdAsync(int id)
    {
        return await _db.Products.Include(c => c.ProductTypes).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Products product)
    {
        await _db.Products.AddAsync(product);
    }
    public async Task UpdateAsync(Products product)
    {
        var existingProduct = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
        if(existingProduct != null)
        {
            _db.Products.Update(product);
        }

    }
    public async Task DeleteAsync(Products product)
    {
        _db.Products.Remove(product);
    }

    public async Task<SelectList> GetProductTypesSelectList()
    {
        var productTypes = await _db.ProductTypes.AsNoTracking().ToListAsync();
        return new SelectList(productTypes, "Id", "ProductType");
    }

    public bool IsExists(string name)
    {
        return _db.Products.Any(c => c.Name == name);
    }

    public async Task<string?> GetImageUrlAsync(int id)
    {
        var book = await _db.Products.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == id);
        return book?.Image;
    }




}

