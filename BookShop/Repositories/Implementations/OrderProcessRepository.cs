using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace BookShop.Repositories.Implementations;

public class OrderProcessRepository : IOrderProcessRepository
{
    private readonly ApplicationDbContext _context;

    public OrderProcessRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.OrderByDescending(d=>d.Id).ToListAsync();
    }



    public async Task<Order?> GetByIdOrderDetailsAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Order_Details)
            .ThenInclude(od => od.Book)
            .FirstOrDefaultAsync(o => o.Id == id);
    }



    public async Task DeleteAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Order_Details)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order != null)
        {
            _context.OrderDetails.RemoveRange(order.Order_Details);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }



    public Task<Order?> GetByIdAsync(int id)
    {
        return _context.Orders
            .Include(o => o.Order_Details)
            .ThenInclude(od => od.Book)
            .FirstOrDefaultAsync(o => o.Id == id);
    }



    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

    }


}
