using BookShop.Data;
using BookShop.Repositories.Interfaces;

namespace BookShop.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IProductTypeRepository ProductTypeRepository { get; }

    public IProductRepository ProductRepository { get; }

    public IOrderProcessRepository OrderProcessRepository { get; }

    public UnitOfWork(
       ApplicationDbContext db,
       IProductTypeRepository productTypeRepo,
       IProductRepository productRepo,
       IOrderProcessRepository orderRepo)
    {
        _db = db;
        ProductTypeRepository = productTypeRepo;
        ProductRepository = productRepo;
        OrderProcessRepository = orderRepo;
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public Task<int> SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
