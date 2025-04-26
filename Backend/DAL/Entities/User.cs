using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("passwordhash")]
    public string Passwordhash { get; set; } = null!;

    [Column("role")]
    [StringLength(20)]
    public string Role { get; set; } = null!;

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column(TypeName = "character varying")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? Phone { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [InverseProperty("User")]
    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();
}
