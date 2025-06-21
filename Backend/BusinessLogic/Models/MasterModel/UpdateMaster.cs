using BusinessLogic.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.MasterModel
{
    public record UpdateMaster(
    int Id,
    string FirstName,
    string LastName,
    string Specialization,
    [EmailAddress] string Email,
    string Password,
    string UserName,
    [Phone] string? PhoneNumber
) : Register(Email, Password, UserName, PhoneNumber);
}
