using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.OrderModels;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ReviewService(IUnitOfWork uof, IMapper mapper) : IReviewService
    {
        public async Task<int> CreateReviewAsync(ReviewModel dto, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var review = mapper.Map<Review>(dto);
            review.ClientId = parsedUserId;

            await uof.ReviewRepository.AddAsync(review);
            return review.Id;
        }

        public async Task DeleteReviewAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var review = await uof.ReviewRepository.GetByIdAsync(id);

            if (review.ClientId != parsedUserId)
            {
                throw new UnauthorizedAccessException("You do not have access to this review");
            }

            await uof.ReviewRepository.DeleteByIdAsync(review.Id);
        }

        public async Task<ReviewModel> GetReviewByIdAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }
            var review = await uof.ReviewRepository.GetByIdAsync(id);
            return mapper.Map<ReviewModel>(review);
        }

    }
}
