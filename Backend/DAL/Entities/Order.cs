using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Order
{
    [Key]
    [Column("OrderID ")]
    public decimal OrderId { get; set; }

    [Column("ClientID ")]
    public decimal ClientId { get; set; }

    [Column("CarID ")]
    public decimal CarId { get; set; }

    [Column("MasterID ")]
    public decimal MasterId { get; set; }

    [Column("OrderDate ")]
    public DateOnly OrderDate { get; set; }

    [Column("Status ", TypeName = "character varying")]
    public string Status { get; set; } = null!;

    [Column("TotalCost ")]
    public decimal TotalCost { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Orders")]
    public virtual Car Car { get; set; } = null!;

    [ForeignKey("ClientId")]
    [InverseProperty("Orders")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("Orders")]
    public virtual Master Master { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    [InverseProperty("Order")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Order")]
    public virtual ICollection<Warrantye> Warrantyes { get; set; } = new List<Warrantye>();
}
