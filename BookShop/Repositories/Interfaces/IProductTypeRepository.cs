using BookShop.Models;

namespace BookShop.Repositories.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductTypes>> GetAllAsync();
        Task<ProductTypes?> GetByIdAsync(int id);
        Task AddAsync(ProductTypes productType);
        Task UpdateAsync(ProductTypes productType);
        Task DeleteAsync(ProductTypes productTypes);

    }
}
