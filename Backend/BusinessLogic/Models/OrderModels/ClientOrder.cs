using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.OrderModels
{
    public class ClientOrder: OrderModel
    {
        public string FullName { get; set; } = string.Empty;
    }
}
