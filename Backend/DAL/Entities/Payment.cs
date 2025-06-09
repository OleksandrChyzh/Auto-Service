using System;
using System.Collections.Generic;

namespace DAL.Entities;

public class Payment : IBaseEntity
{
    // PRIMARY KEY + FOREIGN KEY на Order.Id
    public int Id { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
