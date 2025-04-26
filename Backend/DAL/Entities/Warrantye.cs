using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Warrantye
{
    [Key]
    [Column("warrantyid")]
    public int Warrantyid { get; set; }

    [Column("OrderID ")]
    public decimal OrderId { get; set; }

    [Column("startdate")]
    public DateOnly Startdate { get; set; }

    [Column("enddate")]
    public DateOnly Enddate { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Warrantyes")]
    public virtual Order Order { get; set; } = null!;
}
