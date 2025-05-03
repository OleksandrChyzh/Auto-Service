using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class OrderService : IBaseEntity
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
