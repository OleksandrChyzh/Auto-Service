using DAL.Entities;
using DAL.Interfaces;
using DAL.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.ClientId == userId)
                .Include(o => o.Car)
                .Include(o => o.Master)
                .Include(o => o.Payment)
                .Include(o => o.Review)
                .Include(o => o.OrderServices)
                .ToListAsync();
        }

    }

}
