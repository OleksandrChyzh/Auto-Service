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
            return Ok(await service.GetServicesAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateService dto)
        {
            var id = await service.AddServiceAsync(dto);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await service.DeleteServiceAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] CreateService dto)
        {
            await service.UpdateServiceAsync(dto);
            return NoContent();
        }
    }
}
