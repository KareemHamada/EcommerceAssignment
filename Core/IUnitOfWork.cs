

using Core.Entities;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Order> Orders { get; }
        Task<int> CompleteAsync();
    }
}
