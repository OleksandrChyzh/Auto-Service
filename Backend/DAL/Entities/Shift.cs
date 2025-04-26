using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class Shift
{
    [Key]
    [Column("shiftid")]
    public int Shiftid { get; set; }

    [Column("starttime")]
    public TimeOnly Starttime { get; set; }

    [Column("endtime")]
    public TimeOnly Endtime { get; set; }

    [InverseProperty("Shift")]
    public virtual ICollection<Weeklyschedule> Weeklyschedules { get; set; } = new List<Weeklyschedule>();
}
