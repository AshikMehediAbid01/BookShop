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
        private readonly IProductRepository _repo;
        private readonly IWebHostEnvironment _he;
        public ProductService(IProductRepository repo, IWebHostEnvironment he)
        {
            _repo = repo;
            _he = he;
        }



        public async Task CreateAsync(Products products, IFormFile? Image)
        {
            products.Image = await SaveImageAsync(Image);
            await _repo.AddAsync(products);
        }



        public Task<IEnumerable<Products>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Products?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<string> GetImageUrlAsync(int id) => await _repo.GetImageUrlAsync(id) ?? "Images/NoImageFound.jpg";
        public Task<SelectList> GetProductTypesSelectListAsync() => _repo.GetProductTypesSelectList();
        public bool IsProductExists(string name) => _repo.IsExists(name);
        


        public async Task UpdateAsync(Products product, IFormFile? Image)
        {

            product.Image = (Image != null)
                ? await SaveImageAsync(Image) : await GetImageUrlAsync(product.Id) 
                ?? "Images/NoImageFound.jpg";

                await _repo.UpdateAsync(product);
        }



        public async Task DeleteAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book != null) await _repo.DeleteAsync(book);
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
