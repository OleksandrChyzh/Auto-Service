using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.User
{
    public record UpdateUser(
    string? Password,
    string? UserName,
    [Phone]
    string? PhoneNumber);
}
