using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookShop.Services.Implementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _repo;
        public ProductTypeService(IProductTypeRepository repo)
        {
            _repo = repo;
        }


        public async Task<ProductTypes> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);

            if(category == null)
            {
                throw new KeyNotFoundException($"Category was not found.");
            }
            return category;
        }

        public async Task<IEnumerable<ProductTypes>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task CreateAsync(ProductTypes productType) => await _repo.AddAsync(productType);

        public async Task DeleteAsynce(ProductTypes productType) => await _repo.DeleteAsync(productType);
        
        public async Task UpdateAsync(ProductTypes productType) => await _repo.UpdateAsync(productType);

        
    }
}
