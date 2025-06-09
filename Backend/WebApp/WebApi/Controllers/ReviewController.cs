using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService service) : ControllerBase
    {
        [Authorize(Roles = "Client,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewModel dto)
        {
            var id = await service.CreateReviewAsync(dto, User);
            return Ok(id);
        }


        [Authorize(Roles = "Client,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await service.DeleteReviewAsync(id, User);
            return NoContent();
        }

                
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            return Ok(await service.GetReviewByIdAsync(id, User));
        }
    }
}
