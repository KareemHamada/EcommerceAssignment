using Core;
using Core.Entities;
using Repository.Data;


namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Order> Orders { get; }


        private readonly ECommerceContext _context;
        private bool _disposed = false;

        public UnitOfWork(ECommerceContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Customers = new GenericRepository<Customer>(_context);
            Orders = new GenericRepository<Order>(_context);
        }

       

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
