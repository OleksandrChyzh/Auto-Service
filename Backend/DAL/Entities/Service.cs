using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Table("Service ")]
public partial class Service
{
    [Key]
    [Column("ServiceID ")]
    public decimal ServiceId { get; set; }

    [Column("Name ", TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column("Description ", TypeName = "character varying")]
    public string? Description { get; set; }

    [Column("Price ")]
    public decimal Price { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
