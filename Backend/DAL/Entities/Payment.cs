using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Payment : IBaseEntity
{
    public int Id { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
