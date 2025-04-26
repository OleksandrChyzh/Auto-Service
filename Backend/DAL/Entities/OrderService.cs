using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Table("OrderService ")]
public partial class OrderService
{
    [Key]
    [Column("OrderServiceID ")]
    public decimal OrderServiceId { get; set; }

    [Column("OrderID ")]
    public decimal OrderId { get; set; }

    [Column("ServiceID ")]
    public decimal ServiceId { get; set; }

    [Column("Quantity ")]
    public int Quantity { get; set; }

    [Column("TotalPrice ")]
    public decimal TotalPrice { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderServices")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("OrderServices")]
    public virtual Service Service { get; set; } = null!;
}
