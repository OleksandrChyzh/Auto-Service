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
    public class MasterRepository : Repository<Master>, IMasterRepository
    {
        public MasterRepository(AppDbContext context) : base(context) { }

    }

}
