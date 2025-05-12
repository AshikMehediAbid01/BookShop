using BookShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Products>> GetAllAsync();
    Task<Products?> GetByIdAsync(int id);
    Task AddAsync(Products product);
    Task UpdateAsync(Products product);
    Task DeleteAsync(Products product);
    Task<SelectList> GetProductTypesSelectList();
    bool IsExists(string name);
    Task<string?> GetImageUrlAsync(int id);

}
