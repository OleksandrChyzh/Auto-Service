using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Car
{
    [Key]
    [Column("CarID ")]
    public decimal CarId { get; set; }

    [Column("ClientID ")]
    public decimal ClientId { get; set; }

    [Column("Brand ", TypeName = "character varying")]
    public string Brand { get; set; } = null!;

    [Column("Model ", TypeName = "character varying")]
    public string Model { get; set; } = null!;

    [Column("Year ", TypeName = "character varying")]
    public string Year { get; set; } = null!;

    [Column("LicensePlate ", TypeName = "character varying")]
    public string LicensePlate { get; set; } = null!;

    [Column("VIN ", TypeName = "character varying")]
    public string Vin { get; set; } = null!;

    [ForeignKey("ClientId")]
    [InverseProperty("Cars")]
    public virtual Client Client { get; set; } = null!;

    [InverseProperty("Car")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
