using BookShop.Models;

namespace BookShop.Services.Interfaces;

public interface IProductTypeService
{
    Task<IEnumerable<ProductTypes>> GetAllAsync(); 
    Task<ProductTypes> GetByIdAsync(int id);
    Task CreateAsync(ProductTypes productType);
    Task UpdateAsync(ProductTypes productType);
    Task DeleteAsynce(ProductTypes productType);

}
