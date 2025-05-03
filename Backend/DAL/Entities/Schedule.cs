using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Schedule : IBaseEntity
{
    public int Id { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string WeekDay { get; set; } = null!;

    public int MasterId { get; set; }

    public virtual Master Master { get; set; } = null!;
}
