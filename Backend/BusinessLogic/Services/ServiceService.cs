using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.ServiceModels;
using DAL.Entities;
using DAL.Interfaces;

namespace BusinessLogic.Services
{
    public class ServiceService : GenericService<Service, ServiceModel>, IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        protected override IRepository<Service> _repository => _serviceRepository;

        public ServiceService(IUnitOfWork uof, IMapper mapper) : base(uof, mapper)
        {
            _serviceRepository = uof.ServiceRepository;
        }
    }
}
