using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.ServiceModels;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService service) : ControllerBase
    {
        [Authorize(Roles = "Client,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentModel dto)
        {
            var id = await service.CreatePaymentAsync(dto, User);
            return Ok(id);
        }

        [Authorize(Roles = "Client,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await service.DeletePaymentAsync(id, User);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            return Ok(await service.GetPaymentByIdAsync(id, User));
        }
    }
}
