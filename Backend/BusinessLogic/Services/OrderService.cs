using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.OrderModels;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class OrderService(IUnitOfWork uof, IMapper mapper) : IOrderService
    {
        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid user ID");
            }

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            var orders = await uof.OrderRepository.GetByUserIdAsync(parsedUserId);

            if (roles.Contains("Master"))
            {
                return mapper.Map<IEnumerable<MasterOrder>>(orders);
            }
            else if (roles.Contains("Client") || roles.Contains("Admin"))
            {
                return mapper.Map<IEnumerable<ClientOrder>>(orders);
            }
            else
            {
                throw new UnauthorizedAccessException("Access denied: unsupported role");
            }
        }

        public async Task<int> CreateOrderAsync(CreateOrder dto, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var order = mapper.Map<Order>(dto);
            order.ClientId = parsedUserId;

            await uof.OrderRepository.AddAsync(order);
            return order.Id;
        }

        public async Task DeleteOrderAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var order = await uof.OrderRepository.GetByIdAsync(id);

            if (order.ClientId != parsedUserId)
            {
                throw new UnauthorizedAccessException("You do not have access to this order");
            }

            await uof.OrderRepository.DeleteByIdAsync(order.Id);
        }
    }

}
