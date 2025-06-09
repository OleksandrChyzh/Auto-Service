using BusinessLogic.Models.User;

namespace BusinessLogic.Models.MasterModel
{
    public record GetMaster(
        int Id,
        string FirstName,
        string LastName,
        string Specialization,
        string Email,
        string UserName,
        string? PhoneNumber
    ) : GetUser(Email, UserName, PhoneNumber);
}
