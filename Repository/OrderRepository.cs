using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ECommerceContext _context;
        public OrderRepository(ECommerceContext context) : base(context) {
            _context = context;
        
        }

        public async Task<Order> GetOrderWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
