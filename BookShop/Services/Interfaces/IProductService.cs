using BookShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAllAsync();
        Task<Products?> GetByIdAsync(int id);
        Task CreateAsync(Products products, IFormFile? Image);
        Task UpdateAsync(Products products, IFormFile? Image);
        Task DeleteAsync(int id);
        Task<SelectList> GetProductTypesSelectListAsync();
        Task<string> GetImageUrlAsync(int id);
        bool IsProductExists(string name);


    }
}
