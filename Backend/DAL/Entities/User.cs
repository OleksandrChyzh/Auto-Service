using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace DAL.Entities;

public partial class User : IdentityUser<int>, IBaseEntity
{

    public virtual Master? Master { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

}
