using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.MasterModel;
using BusinessLogic.Models.ServiceModels;
using DAL.Entities;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class MasterService : IMasterService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uof;

        public MasterService(
            UserManager<User> userManager,
            AppDbContext context,
            IMapper mapper,
            IUnitOfWork uof)
        {
            _userManager = userManager;
            _context = context;
            this.mapper = mapper;
            this.uof = uof;
        }

        public async Task<IEnumerable<GetMaster>> GetMastersAsync()
        {
            var masters = await uof.MasterRepository.GetAllAsync();
            return mapper.Map<IEnumerable<GetMaster>>(masters);
        }

        public async Task<int> AddMasterAsync(AddMaster dto)
        {
            // 1. Перевірка — чи такий UserName вже існує
            var existingUser = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                throw new Exception($"Користувач з іменем '{dto.UserName}' вже існує.");
            }

            // 2. Створюємо нового користувача
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Помилка при створенні користувача: " +
                    string.Join(", ", result.Errors.Select(static e => e.Description)));
            }

            try
            {
                // 3. Додаємо роль
                await _userManager.AddToRoleAsync(user, "Master");

                // 4. Створюємо майстра
                var master = new Master
                {
                    Id = user.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Specialization = dto.Specialization,
                    User = user
                };

                await uof.MasterRepository.AddAsync(master);
                return master.Id;
            }
            catch
            {
                // ❗Важливо: очищення, якщо щось пішло не так
                await _userManager.DeleteAsync(user);
                throw;
            }
        }


        public async Task DeleteMasterAsync(int id)
        {
            // 1. Перевірка, чи існує майстер
            var master = await uof.MasterRepository.GetByIdAsync(id);
            if (master == null)
            {
                throw new Exception($"Майстер з Id {id} не знайдений.");
            }

            // 2. Видаляємо майстра
            await uof.MasterRepository.DeleteByIdAsync(id);

            // 3. Знаходимо користувача за тим же id (User.Id == Master.Id)
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception($"Користувач з Id {id} не знайдений.");
            }

            // 4. Видаляємо користувача
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Помилка при видаленні користувача: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

    }

}
