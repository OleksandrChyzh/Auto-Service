using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.CarModel
{
    public class CreateCar
    {
        

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public string Year { get; set; } = string.Empty;

        [Required]
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        public string Vin { get; set; } = string.Empty;
    }
}
