using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookShop.Services.Implementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ProductTypes> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);

            if(category == null)
            {
                throw new KeyNotFoundException($"Category was not found.");
            }
            return category;
        }

        public async Task<IEnumerable<ProductTypes>> GetAllAsync() => await _unitOfWork.ProductTypeRepository.GetAllAsync();

        public async Task CreateAsync(ProductTypes productType)
        {
            await _unitOfWork.ProductTypeRepository.AddAsync(productType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductTypes productType)
        {
            await _unitOfWork.ProductTypeRepository.DeleteAsync(productType);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task UpdateAsync(ProductTypes productType)
        {
            await _unitOfWork.ProductTypeRepository.UpdateAsync(productType);
            await _unitOfWork.SaveChangesAsync();
        }


    }
}
