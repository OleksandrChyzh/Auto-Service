using BusinessLogic.Models.ScheduleModel;
using BusinessLogic.Models.ServiceModels;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<GetSchedule>> GetSchedulesAsync(ClaimsPrincipal user);
        Task<IEnumerable<GetSchedule>> GetSchedulesByMasterIdAsync(int id);

        Task<int> AddAsync(CreateSchedule dto);

        Task DeleteAsync(int id);

        Task UpdateAsync(UpdateSchedule dto);
    }
}
