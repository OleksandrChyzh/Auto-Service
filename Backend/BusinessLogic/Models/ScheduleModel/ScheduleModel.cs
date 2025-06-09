using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.ScheduleModel
{
    public abstract class ScheduleModel
    {
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string WeekDay { get; set; } = string.Empty;
    }
}
