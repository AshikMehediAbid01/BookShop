using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;

namespace BookShop.Services.Implementations;

public class OrderProcessService : IOrderProcessService
{
    private readonly IOrderProcessRepository _repo;
    public OrderProcessService(IOrderProcessRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() => await _repo.GetAllAsync();
    
    public async Task<Order?> GetByIdAsync(int id) => await _repo.GetByIdOrderDetailsAsync(id);

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

    public async Task UpdateAsync(int id, string status)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order != null)
        {
            order.Status = status;
            await _repo.UpdateAsync(order);
        }
    }






}
