using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Payment
{
    [Key]
    [Column("paymentid")]
    public int Paymentid { get; set; }

    [Column("OrderID ")]
    public decimal OrderId { get; set; }

    [Column("paymentdate")]
    public DateOnly Paymentdate { get; set; }

    [Column("amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("paymentmethod")]
    [StringLength(50)]
    public string Paymentmethod { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("Payments")]
    public virtual Order Order { get; set; } = null!;
}
