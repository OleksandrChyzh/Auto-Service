using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Models;
using BusinessLogic.Models.ServiceModels;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IServiceService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            return Ok(await service.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateService dto)
        {
            var id = await service.AddAsync(dto);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
