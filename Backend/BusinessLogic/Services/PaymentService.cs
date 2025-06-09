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
    public class PaymentService(IUnitOfWork uof, IMapper mapper) : IPaymentService
    {
        public async Task<int> CreatePaymentAsync(PaymentModel dto, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out _))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }
            var payment = mapper.Map<Payment>(dto);

            await uof.PaymentRepository.AddAsync(payment);
            return payment.Id;
        }

        public async Task DeletePaymentAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var payment = await uof.PaymentRepository.GetByIdAsync(id);

            if (payment.Order.ClientId != parsedUserId)
            {
                throw new UnauthorizedAccessException("You do not have access to this payment");
            }

            await uof.PaymentRepository.DeleteByIdAsync(payment.Id);
        }

        public async Task<PaymentModel> GetPaymentByIdAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out _))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }
            var payment = await uof.PaymentRepository.GetByIdAsync(id);
            return mapper.Map<PaymentModel>(payment);
        }

    }
}
