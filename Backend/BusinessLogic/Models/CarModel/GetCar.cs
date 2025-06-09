using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.CarModel
{
    public class GetCar
    {
        public int Id { get; set; }

        public string Brand { get; set; } = string.Empty;
        
        public string Model { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        public string LicensePlate { get; set; } = string.Empty;

        public string Vin { get; set; } = string.Empty;
    }
}
