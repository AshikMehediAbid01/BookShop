using BookShop.Models;

namespace BookShop.Repositories.Interfaces
{
    public interface IOrderProcessRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task <Order?> GetByIdOrderDetailsAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        void Update(Order order);
        Task DeleteAsync(int id);

    }
}
 