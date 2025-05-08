using BookShop.Models;

namespace BookShop.Repositories.Interfaces
{
    public interface IOrderProcessRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task <Order?> GetByIdOrderDetailsAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);

    }
}
