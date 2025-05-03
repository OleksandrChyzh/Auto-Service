using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Review : IBaseEntity
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateOnly ReviewDate { get; set; }

    public virtual User Client { get; set; } = null!;
}
