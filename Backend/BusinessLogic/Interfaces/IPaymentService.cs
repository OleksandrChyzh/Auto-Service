using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePaymentAsync(PaymentModel dto, ClaimsPrincipal user);

        Task DeletePaymentAsync(int id, ClaimsPrincipal user);

        Task<PaymentModel> GetPaymentByIdAsync(int id, ClaimsPrincipal user);

    }
}
