using BusinessLogic.Models.ScheduleModel;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IScheduleService : IGenericService<ScheduleModel>
    {
        Task<IEnumerable<GetSchedule>> GetSchedulesAsync(ClaimsPrincipal user);
        Task<IEnumerable<GetSchedule>> GetSchedulesByMasterIdAsync(int id);
    }
}
