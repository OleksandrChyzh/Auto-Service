using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(ICarService service) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok(await service.GetCarsAsync(User));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CreateCar dto)
        {
            var id = await service.AddCarAsync(dto, User);
            return Ok(id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await service.DeleteCarAsync(id, User);
            return NoContent();
        }

    }
}
