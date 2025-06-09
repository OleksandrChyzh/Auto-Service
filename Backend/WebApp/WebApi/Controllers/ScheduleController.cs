using BusinessLogic.Interfaces;
using BusinessLogic.Models.ScheduleModel;
using BusinessLogic.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IScheduleService service) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateSchedule dto)
        {
            var id = await service.AddAsync(dto);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Master")]
        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            return Ok(await service.GetSchedulesAsync(User));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedulesByMasterId(int id)
        {
            return Ok(await service.GetSchedulesByMasterIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody] CreateSchedule dto)
        {
            await service.UpdateAsync(dto);
            return NoContent();
        }

    }
}
