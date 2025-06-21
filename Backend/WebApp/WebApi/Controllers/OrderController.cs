using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Models;
using BusinessLogic.Models.ServiceModels;
using BusinessLogic.Models.OrderModels;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService service) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await service.GetOrdersAsync(User));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder dto)
        {
            var id = await service.CreateOrderAsync(dto, User);
            return Ok(id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await service.DeleteOrderAsync(id, User);
            return NoContent();
        }

       
        [Authorize(Roles = "Master")]
        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            await service.AcceptOrderAsync(id, User);
            return NoContent();
        }

        
        [Authorize(Roles = "Master")]
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            await service.CompleteOrderAsync(id, User);
            return NoContent();
        }
    }
}
