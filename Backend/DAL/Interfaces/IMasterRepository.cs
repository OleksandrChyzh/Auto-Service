using DAL.Entities;
using DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMasterRepository : IRepository<Master>
    {
        Task<Master> GetByMasterIdAsync(int id);

    }
}
