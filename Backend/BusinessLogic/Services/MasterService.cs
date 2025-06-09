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

        public MasterService(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<GetMaster>> GetMastersAsync()
        {
            var masters = await uof.MasterRepository.GetAllAsync();
            return mapper.Map<IEnumerable<GetMaster>>(masters);
        }

        public async Task<int> AddMasterAsync(AddMaster dto)
        {
            // 1. Створюємо користувача
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
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // 2. Додаємо роль
            await _userManager.AddToRoleAsync(user, "Master");

            // 3. Створюємо майстра
            var master = new Master
            {
                Id = user.Id, // Встановлюємо той самий Id
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Specialization = dto.Specialization,
                User = user
            };

            _context.Masters.Add(master);
            await _context.SaveChangesAsync();

            return master.Id;
        }

        public async Task DeleteMasterAsync(int id)
        {
            // 1. Знаходимо майстра з включенням користувача
            var master = await _context.Masters
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (master == null)
            {
                throw new Exception($"Майстер з Id {id} не знайдений.");
            }

            // 2. Видаляємо користувача (каскадно видалиться Master, якщо налаштовано)
            var result = await _userManager.DeleteAsync(master.User);

            if (!result.Succeeded)
            {
                throw new Exception("Помилка при видаленні користувача: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        public async Task<int> UpdateMasterAsync(AddMaster dto)
        {
            // 1. Знаходимо майстра з включенням користувача
            var master = await _context.Masters
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == dto.Id);

            if (master == null)
            {
                throw new Exception($"Майстер з Id {dto.Id} не знайдений.");
            }

            // 2. Оновлюємо користувача
            master.User.Email = dto.Email;
            master.User.UserName = dto.UserName;
            master.User.PhoneNumber = dto.PhoneNumber;

            // Якщо пароль оновлюється
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(master.User);
                var result = await _userManager.ResetPasswordAsync(master.User, token, dto.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("Помилка при оновленні пароля: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // 3. Оновлюємо Master
            master.FirstName = dto.FirstName;
            master.LastName = dto.LastName;
            master.Specialization = dto.Specialization;

            _context.Masters.Update(master);
            await _context.SaveChangesAsync();

            return master.Id;
        }

    }

}
