

using BusinessLogic.Models.MasterModel;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.ScheduleModel;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController(IMasterService service) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetMasters()
        {
            return Ok(await service.GetMastersAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMaster([FromBody] AddMaster dto)
        {
            var id = await service.AddMasterAsync(dto);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaster(int id)
        {
            await service.DeleteMasterAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMaster([FromBody] AddMaster dto)
        {
            await service.UpdateMasterAsync(dto);
            return NoContent();
        }
    }
}
