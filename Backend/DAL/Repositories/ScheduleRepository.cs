using DAL.Entities;
using DAL.Interfaces;
using DAL.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context) { }
    }
}
