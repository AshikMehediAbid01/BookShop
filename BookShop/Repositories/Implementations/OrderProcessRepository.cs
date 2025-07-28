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


    public async Task<IEnumerable<Order>> GetAllAsync(string status)
    {
        var orders = _context.Orders.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(status) && status != "All")
            orders = orders.Where(o => o.Status == status)
                .OrderByDescending(o=>o.OrderDate);

        return await orders.ToListAsync();

/*
        return await _context.Orders.AsNoTracking()
            .OrderByDescending(d=>d.Id).ToListAsync();*/
    }



    public async Task<Order?> GetByIdOrderDetailsAsync(int id)
    {
        return await _context.Orders
            .AsNoTracking()
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
        }

    }



    public Task<Order?> GetByIdAsync(int id)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(o => o.Order_Details)
            .ThenInclude(od => od.Book)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public void Update(Order order)
    {
        if (order != null)
        {
            _context.Orders.Update(order);
        }
    }

}
