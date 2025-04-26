using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Weeklyschedule
{
    [Key]
    [Column("scheduleid")]
    public int Scheduleid { get; set; }

    [Column("MasterID ")]
    public decimal MasterId { get; set; }

    [Column("weekday")]
    public int Weekday { get; set; }

    [Column("shiftid")]
    public int Shiftid { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("Weeklyschedules")]
    public virtual Master Master { get; set; } = null!;

    [ForeignKey("Shiftid")]
    [InverseProperty("Weeklyschedules")]
    public virtual Shift Shift { get; set; } = null!;
}
