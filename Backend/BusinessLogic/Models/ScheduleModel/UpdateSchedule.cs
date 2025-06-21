using BusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.ScheduleModel
{
    public class UpdateSchedule : ScheduleModel
    {
        public int Id { get; set; }

        [Required]
        public new TimeOnly StartTime { get; set; }

        [Required]
        public new TimeOnly EndTime { get; set; }

        [Required]
        [WeekDay]
        public new string WeekDay { get; set; } = string.Empty;
    }
}
