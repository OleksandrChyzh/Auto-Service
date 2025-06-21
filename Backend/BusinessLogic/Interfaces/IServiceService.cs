using BusinessLogic.Models.OrderModels;
using BusinessLogic.Models.ScheduleModel;
using BusinessLogic.Models.ServiceModels;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<GetService>> GetServicesAsync();

        Task<int> AddServiceAsync(CreateService dto);

        Task DeleteServiceAsync(int id);

        Task UpdateServiceAsync(CreateService dto);
    }
}
