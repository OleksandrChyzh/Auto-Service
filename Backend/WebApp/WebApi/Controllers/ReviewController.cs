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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewModel dto)
        {
            if (User.IsInRole("Master") || User.IsInRole("Admin"))
                return Forbid("Only clients are allowed to create reviews.");

            var id = await service.CreateReviewAsync(dto, User);
            return Ok(id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (User.IsInRole("Master") || User.IsInRole("Admin"))
                return Forbid("Only clients are allowed to delete reviews.");

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
