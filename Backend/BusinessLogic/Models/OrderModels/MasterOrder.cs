using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.OrderModels
{
    public class MasterOrder: OrderModel
    {
        public string UserName { get; set; } = null!;
    }
}
