using BusinessLogic.Models.User;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IAuthorizationService
    {
        Task<(string Token, string[] Roles)> LoginAsync(string email, string password);

        Task<string> RegisterAsync(Register dto);

        Task<GetUser> GetUserByIdAsync(ClaimsPrincipal userClaims);

        Task UpdateUserAsync(UpdateUser dto, ClaimsPrincipal userClaims);
    }

}
