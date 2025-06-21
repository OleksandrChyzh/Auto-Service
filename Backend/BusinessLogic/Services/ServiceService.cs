using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.ServiceModels;
using DAL.Entities;
using DAL.Interfaces;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class ServiceService(IUnitOfWork uof, IMapper mapper) : IServiceService
    {
        public async Task<IEnumerable<GetService>> GetServicesAsync()
        {

            var services = await uof.ServiceRepository.GetAllAsync();
            return mapper.Map<IEnumerable<GetService>>(services);
        }
        public async Task<int> AddServiceAsync(CreateService dto)
        {
            var service = mapper.Map<Service>(dto);

            await uof.ServiceRepository.AddAsync(service);
            return service.Id;
        }

        public async Task DeleteServiceAsync(int id)
        {
            
            var service = await uof.ServiceRepository.GetByIdAsync(id);
            await uof.ServiceRepository.DeleteByIdAsync(service.Id);
        }

        public async Task UpdateServiceAsync(CreateService dto)
        {

            var service = mapper.Map<Service>(dto);
            await uof.ServiceRepository.UpdateAsync(service);
        }
    }
}

