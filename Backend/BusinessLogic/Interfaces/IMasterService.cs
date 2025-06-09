using BusinessLogic.Models.MasterModel;
using BusinessLogic.Models.ScheduleModel;

namespace BusinessLogic.Interfaces
{
    public interface IMasterService 
    {
        Task<IEnumerable<GetMaster>> GetMastersAsync();
        Task<int> AddMasterAsync(AddMaster dto);
        Task DeleteMasterAsync(int id);

        Task<int> UpdateMasterAsync(AddMaster dto);

    }
}
