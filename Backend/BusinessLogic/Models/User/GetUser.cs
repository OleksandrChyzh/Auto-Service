using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.User
{
    public record GetUser(
    string Email,
    string UserName,
    string? PhoneNumber);
}
