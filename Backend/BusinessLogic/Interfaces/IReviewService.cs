using BusinessLogic.Models;
using DAL.Entities;
using System.Security.Claims;


namespace BusinessLogic.Interfaces
{
    public interface IReviewService
    {
        Task<int> CreateReviewAsync(ReviewModel dto, ClaimsPrincipal user);

        Task DeleteReviewAsync(int id, ClaimsPrincipal user);

        Task<ReviewModel> GetReviewByIdAsync(int id, ClaimsPrincipal user);

    }
}
