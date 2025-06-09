using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IGenericService<TModel>
        where TModel : class
    {
        Task<int> AddAsync(TModel model);
        Task DeleteAsync(int modelId);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
        Task UpdateAsync(TModel model);
    }
}
