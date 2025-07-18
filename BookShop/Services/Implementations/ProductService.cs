using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using static System.Net.Mime.MediaTypeNames;

namespace BookShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _he;
        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment he)
        {
            _unitOfWork = unitOfWork;
            _he = he;
        }



        public async Task CreateAsync(Products products, IFormFile? Image)
        {
            products.Image = await SaveImageAsync(Image);
            await _unitOfWork.ProductRepository.AddAsync(products);
            await _unitOfWork.SaveChangesAsync();
        }



        public Task<IEnumerable<Products>> GetAllAsync() => _unitOfWork.ProductRepository.GetAllAsync();
        public Task<Products?> GetByIdAsync(int id) => _unitOfWork.ProductRepository.GetByIdAsync(id);
        public async Task<string> GetImageUrlAsync(int id) => await _unitOfWork.ProductRepository.GetImageUrlAsync(id) ?? "Images/NoImageFound.jpg";
        public Task<SelectList> GetProductTypesSelectListAsync() => _unitOfWork.ProductRepository.GetProductTypesSelectList();
        public bool IsProductExists(string name) => _unitOfWork.ProductRepository.IsExists(name);
        


        public async Task UpdateAsync(Products product, IFormFile? Image)
        {

            product.Image = (Image != null)
                ? await SaveImageAsync(Image) : await GetImageUrlAsync(product.Id) 
                ?? "Images/NoImageFound.jpg";

                await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var book = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (book != null) await _unitOfWork.ProductRepository.DeleteAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }




        private async Task<string> SaveImageAsync(IFormFile? image)
        {
            if (image == null) return "Images/NoImageFound.jpg";

            var imagePath = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(image.FileName));
            using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return "Images/" + image.FileName;

        }
    }
}
