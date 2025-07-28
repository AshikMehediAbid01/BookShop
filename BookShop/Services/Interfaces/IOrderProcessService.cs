using System.Globalization;
using BookShop.Models;

namespace BookShop.Services.Interfaces
{
    public interface IOrderProcessService
    {
        Task<IEnumerable<Order>> GetAllAsync(string status);
        Task<Order?> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, String status);


    }
}
