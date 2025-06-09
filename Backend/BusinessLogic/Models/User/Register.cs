using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.User
{
    public record Register
(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [Range(6, 100)]
    string Password,

    [Required]
    string UserName,

    [Phone]
    string? PhoneNumber);
}
