using System;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Attributes;

namespace BusinessLogic.Models.ScheduleModel
{
    public class CreateSchedule : ScheduleModel
    {
        public int MasterId { get; set; }

        [Required]
        public new TimeOnly StartTime { get; set; }

        [Required]
        public new TimeOnly EndTime { get; set; }

        [Required]
        [WeekDay]
        public new string WeekDay { get; set; } = string.Empty;
    }
}
