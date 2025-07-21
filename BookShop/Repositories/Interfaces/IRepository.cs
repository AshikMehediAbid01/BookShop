using BookShop.Models;

namespace BookShop.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(T obj);

}

