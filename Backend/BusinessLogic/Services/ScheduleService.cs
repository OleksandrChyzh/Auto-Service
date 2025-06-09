using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.ScheduleModel;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ScheduleService : GenericService<Schedule, ScheduleModel>, IScheduleService
    {
        protected override IRepository<Schedule> _repository => _uof.ScheduleRepository;

        public ScheduleService(IUnitOfWork uof, IMapper mapper)
            : base(uof, mapper)
        {
        }

        public async Task<IEnumerable<GetSchedule>> GetSchedulesAsync(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var schedules = await _uof.ScheduleRepository.GetByMasterIdAsync(parsedUserId);
            return _mapper.Map<IEnumerable<GetSchedule>>(schedules);
        }

        public async Task<IEnumerable<GetSchedule>> GetSchedulesByMasterIdAsync(int id)
        {
            var schedules = await _uof.ScheduleRepository.GetByMasterIdAsync(id);
            return _mapper.Map<IEnumerable<GetSchedule>>(schedules);
        }
    }
}
