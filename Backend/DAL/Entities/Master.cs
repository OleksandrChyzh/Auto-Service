using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Master : IBaseEntity
{
    public int Id { get; set; }

    public string Specialization { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User User { get; set; } = null!;
}
