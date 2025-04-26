using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Client
{
    [Key]
    [Column("ClientID ")]
    public decimal ClientId { get; set; }

    [Column("FullName ", TypeName = "character varying")]
    public string FullName { get; set; } = null!;

    public int UserId { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    [InverseProperty("Client")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("Clients")]
    public virtual User User { get; set; } = null!;
}
