using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.User;
using DAL.Entities;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using DAL.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace BusinessLogic.Services
{
    public class AuthorizationService(
    AppDbContext context,
    UserManager<User> userManager,
    IConfiguration configuration,
    IMapper mapper) : IAuthorizationService
    {
        public async Task<(string Token, string[] Roles)> LoginAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null || !await userManager.CheckPasswordAsync(user, password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = await GenerateJwtToken(user);
            var roles = await userManager.GetRolesAsync(user);

            return (token, roles.ToArray());
        }

        public async Task<string> RegisterAsync(Register dto)
        {
            var user = mapper.Map<User>(dto);
            var result = await userManager.CreateAsync(user, dto.Password);

            ManageIdentityException.Throw(result);
            return await GenerateJwtToken(user);
        }

        public async Task<GetUser> GetUserByIdAsync(ClaimsPrincipal userClaims)
        {
            return mapper.Map<GetUser>(await GetUser(userClaims));
        }

        public async Task UpdateUserAsync(UpdateUser dto, ClaimsPrincipal userClaims)
        {
            var user = await GetUser(userClaims);

            if (dto.Password is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, dto.Password);
                ManageIdentityException.Throw(result);
            }

            user.UserName = dto.UserName ?? user.UserName;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id.ToString()),
        };

            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray());

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> GetUser(ClaimsPrincipal userClaims)
        {
            var id = int.Parse(userClaims.FindFirstValue(ClaimTypes.Sid)
                ?? throw new NotFoundException(nameof(User), nameof(User.Id), userClaims.FindFirstValue(ClaimTypes.Sid)!));

            return await userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), nameof(User.Id), id.ToString());
        }
    }
}
