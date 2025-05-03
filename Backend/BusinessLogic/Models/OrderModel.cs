using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateOnly OrderDate { get; set; }

        public string Status { get; set; } = null!;

        public decimal TotalCost { get; set; }

    }
}
