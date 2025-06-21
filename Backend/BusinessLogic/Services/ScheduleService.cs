using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.ScheduleModel;
using BusinessLogic.Models.ServiceModels;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ScheduleService(IUnitOfWork uof, IMapper mapper) : IScheduleService
    {
        
        public async Task<IEnumerable<GetSchedule>> GetSchedulesAsync(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var schedules = await uof.ScheduleRepository.GetByMasterIdAsync(parsedUserId);
            return mapper.Map<IEnumerable<GetSchedule>>(schedules);
        }

        public async Task<IEnumerable<GetSchedule>> GetSchedulesByMasterIdAsync(int id)
        {
            var schedules = await uof.ScheduleRepository.GetByMasterIdAsync(id);
            return mapper.Map<IEnumerable<GetSchedule>>(schedules);
        }

        public async Task<int> AddAsync(CreateSchedule dto)
        {
            var schedule = mapper.Map<Schedule>(dto);

            await uof.ScheduleRepository.AddAsync(schedule);
            return schedule.Id;
        }

        public async Task DeleteAsync(int id)
        {

            var schedule = await uof.ScheduleRepository.GetByIdAsync(id);
            await uof.ScheduleRepository.DeleteByIdAsync(schedule.Id);
        }

        public async Task UpdateAsync(UpdateSchedule dto)
        {
            // Завантажуємо існуючий розклад із бази
            var schedule = await uof.ScheduleRepository.GetByIdAsync(dto.Id)
                          ?? throw new Exception("Schedule not found");

            // Мапимо нові значення з dto у вже завантажену сутність
            mapper.Map(dto, schedule);

            // Оновлюємо розклад
            await uof.ScheduleRepository.UpdateAsync(schedule);
        }

    }
}
