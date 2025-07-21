using BookShop.Data;
using BookShop.Models;
using BookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> _table;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _table = db.Set<T>();
    }

    public async Task AddAsync(T obj)
    {
        await _table.AddAsync(obj);
    }

    public Task DeleteAsync(T obj)
    {
        _table.Remove(obj);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _table.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

    public  Task UpdateAsync(T obj)
    {
        _table.Update(obj);
        return Task.CompletedTask;
    }
}
