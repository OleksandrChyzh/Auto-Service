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
    public class MasterRepository : Repository<Master>, IMasterRepository
    {
        public MasterRepository(AppDbContext context) : base(context) { }

        public async Task<Master?> GetByMasterIdAsync(int id)
        {
            return await _context.Masters
                .Include(m => m.User)
                .Include(m => m.Orders)
                .Include(m => m.Schedules)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }

}
