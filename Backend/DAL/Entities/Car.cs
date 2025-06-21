using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public partial class Car: IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Year { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public int ClientId { get; set; }
    public virtual User Client { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
