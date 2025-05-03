using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Year { get; set; } = null!;

        public string LicensePlate { get; set; } = null!;

        public string Vin { get; set; } = null!;
    }
}
