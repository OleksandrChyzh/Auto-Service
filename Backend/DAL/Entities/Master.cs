using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Table("Master ")]
public partial class Master
{
    [Key]
    [Column("MasterID ")]
    public decimal MasterId { get; set; }

    [Column("FullName ", TypeName = "character varying")]
    public string FullName { get; set; } = null!;

    [Column("Specialization ", TypeName = "character varying")]
    public string Specialization { get; set; } = null!;

    public int UserId { get; set; }

    [InverseProperty("Master")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("Masters")]
    public virtual User User { get; set; } = null!;

    [InverseProperty("Master")]
    public virtual ICollection<Weeklyschedule> Weeklyschedules { get; set; } = new List<Weeklyschedule>();
}
