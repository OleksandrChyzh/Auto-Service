using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ReviewModel
    {

        [Required]
        public int Rating { get; set; }

        [Required]
        public int OrderId { get; set; }

        public string? Comment { get; set; } = string.Empty;
    }
}
