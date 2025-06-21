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
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Schedule>> GetByMasterIdAsync(int masterId)
        {
            return await _context.Schedules
                .Where(s => s.MasterId == masterId)
                .Include(s => s.Master)
                .ToListAsync();
        }

    }
}
