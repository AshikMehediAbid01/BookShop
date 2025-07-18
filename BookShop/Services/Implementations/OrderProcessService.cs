using BookShop.Models;
using BookShop.Repositories.Interfaces;
using BookShop.Services.Interfaces;

namespace BookShop.Services.Implementations;

public class OrderProcessService : IOrderProcessService
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderProcessService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() => await _unitOfWork.OrderProcessRepository.GetAllAsync();
    
    public async Task<Order?> GetByIdAsync(int id) => await _unitOfWork.OrderProcessRepository.GetByIdOrderDetailsAsync(id);

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.OrderProcessRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, string status)
    {
        var order = await _unitOfWork.OrderProcessRepository.GetByIdAsync(id);
        if (order != null)
        {
            order.Status = status;
            _unitOfWork.OrderProcessRepository.Update(order);
        }
        await _unitOfWork.SaveChangesAsync();
    }






}
