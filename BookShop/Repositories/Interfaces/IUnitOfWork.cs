namespace BookShop.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductTypeRepository ProductTypeRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderProcessRepository OrderProcessRepository { get; }
    Task<int> SaveChangesAsync();
}
