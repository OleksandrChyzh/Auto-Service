using BusinessLogic.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.MasterModel
{
    public record AddMaster(
    [Required] int Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Specialization,

    [Required][EmailAddress] string Email,
    [Required][Range(6, 100)] string Password,
    [Required] string UserName,
    [Phone] string? PhoneNumber
) : Register(Email, Password, UserName, PhoneNumber);
}
