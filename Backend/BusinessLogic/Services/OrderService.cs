using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.OrderModels;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
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


            if (roles.Contains("Master"))
            {
                var orders = await uof.OrderRepository.GetByMasterIdAsync(parsedUserId);

                return mapper.Map<IEnumerable<MasterOrder>>(orders);
            }
            else 
            {
                var orders = await uof.OrderRepository.GetByUserIdAsync(parsedUserId);

                return mapper.Map<IEnumerable<ClientOrder>>(orders);
            }
            
        }

        public async Task<int> CreateOrderAsync(CreateOrder dto, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            if (dto.ServiceIds == null || dto.ServiceIds.Count == 0)
            {
                throw new ArgumentException("Order must contain at least one service.");
            }

            var order = mapper.Map<Order>(dto);
            order.ClientId = parsedUserId;
            order.Status = "Створене";
            order.OrderDate = DateOnly.FromDateTime(DateTime.UtcNow);
            order.TotalCost = 0m;

            var services = new List<Service>();

            foreach (var serviceId in dto.ServiceIds)
            {
                var service = await uof.ServiceRepository.GetByIdAsync(serviceId);
                if (service == null)
                {
                    throw new Exception($"Сервіс з ID {serviceId} не знайдено.");
                }

                services.Add(service);
            }

            foreach (var service in services)
            {
                order.OrderServices.Add(new DAL.Entities.OrderService
                {
                    ServiceId = service.Id
                });

                order.TotalCost += service.Price;
            }

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

        public async Task AcceptOrderAsync(int orderId, ClaimsPrincipal user)
        {
            var sidClaim = user.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(sidClaim) || !int.TryParse(sidClaim, out var masterId))
                throw new UnauthorizedAccessException("Invalid master ID");

            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)
                                   .Select(c => c.Value);
            if (!roles.Contains("Master"))
                throw new UnauthorizedAccessException("Only masters can accept orders");

            var order = await uof.OrderRepository.GetByIdAsync(orderId)
                         ?? throw new Exception("Order not found");

            if (order.MasterId != masterId)
                throw new UnauthorizedAccessException("Not your order");

            if (order.Status != "Створене")
                throw new InvalidOperationException("Order can only be accepted from status 'Створене'");

            order.Status = "Прийняте";

            await uof.OrderRepository.UpdateAsync(order); 
        }


        public async Task CompleteOrderAsync(int orderId, ClaimsPrincipal user)
        {
            var sidClaim = user.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(sidClaim) || !int.TryParse(sidClaim, out var masterId))
                throw new UnauthorizedAccessException("Invalid master ID");

            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)
                                   .Select(c => c.Value);
            if (!roles.Contains("Master"))
                throw new UnauthorizedAccessException("Only masters can complete orders");

            var order = await uof.OrderRepository.GetByIdAsync(orderId)
                         ?? throw new Exception("Order not found");

            if (order.MasterId != masterId)
                throw new UnauthorizedAccessException("Not your order");

            if (order.Status != "Прийняте")
                throw new InvalidOperationException("Order must first be accepted (status 'Прийняте')");

            order.Status = "Закінчене";

            await uof.OrderRepository.UpdateAsync(order); // ← оновлюємо через репозиторій
        }



    }

}
