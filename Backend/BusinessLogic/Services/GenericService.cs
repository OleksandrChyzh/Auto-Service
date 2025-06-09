using BusinessLogic.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public abstract class GenericService<TEntity, TModel> : IGenericService<TModel>
        where TEntity : class, IBaseEntity
        where TModel : class
    {
        protected abstract IRepository<TEntity> _repository { get; }

        protected readonly IUnitOfWork _uof;
        protected readonly IMapper _mapper;

        protected GenericService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public virtual async Task<int> AddAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _uof.SaveAsync();

            return entity.Id; 
        }

        public virtual Task DeleteAsync(int modelId)
        {
            return _repository.DeleteByIdAsync(modelId)
                .ContinueWith(t => _uof.SaveAsync());
        }

        public async virtual Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual Task<TModel> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id)
                .ContinueWith(t => _mapper.Map<TModel>(t.Result));
        }

        public virtual Task UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _repository.UpdateAsync(entity);
            return _uof.SaveAsync();
        }
    }
}
