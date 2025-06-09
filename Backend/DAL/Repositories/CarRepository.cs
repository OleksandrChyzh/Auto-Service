using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Data;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Car>> GetByUserIdAsync(int userId)
        {
            return await _context.Cars
        .Where(c => c.ClientId == userId)
        .Include(c => c.Client)      
        .Include(c => c.Orders)      
        .ToListAsync();
        }

    }
}
