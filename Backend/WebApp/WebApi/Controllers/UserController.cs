using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using BusinessLogic.Models.User;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController(BusinessLogic.Interfaces.IAuthorizationService service) : ControllerBase
    {
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            return Ok(await service.GetUserByIdAsync(User));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            return Ok(await service.RegisterAsync(dto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login dto)
        {
            var (token, roles) = await service.LoginAsync(dto.Email, dto.Password);
            return Ok(new { token, roles });
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUser dto)
        {
            await service.UpdateUserAsync(dto, User);
            return NoContent();
        }
    }
}
