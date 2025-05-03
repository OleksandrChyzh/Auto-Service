using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Order : IBaseEntity
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalCost { get; set; }

    public int ClientId { get; set; }

    public int CarId { get; set; }

    public int MasterId { get; set; }

    public int? PaymentId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual User Client { get; set; } = null!;

    public virtual Master Master { get; set; } = null!;

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual Payment? Payment { get; set; }
}
