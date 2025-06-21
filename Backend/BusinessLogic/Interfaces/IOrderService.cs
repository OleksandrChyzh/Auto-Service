using BusinessLogic.Models.OrderModels;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetOrdersAsync(ClaimsPrincipal user);

        Task<int> CreateOrderAsync(CreateOrder dto, ClaimsPrincipal user);

        Task DeleteOrderAsync(int id, ClaimsPrincipal user);

        Task AcceptOrderAsync(int orderId, ClaimsPrincipal user);
        Task CompleteOrderAsync(int orderId, ClaimsPrincipal user);
    }
}
