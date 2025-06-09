using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.CarModel;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CarService(IUnitOfWork uof, IMapper mapper) : ICarService
    {
        public async Task<IEnumerable<GetCar>> GetCarsAsync(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var cars = await uof.CarRepository.GetByUserIdAsync(parsedUserId);
            return mapper.Map<IEnumerable<GetCar>>(cars);
        }

        public async Task<int> AddCarAsync(CreateCar dto, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var car = mapper.Map<Car>(dto);
            car.ClientId = parsedUserId;

            await uof.CarRepository.AddAsync(car);
            return car.Id;
        }

        public async Task DeleteCarAsync(int id, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid client ID");
            }

            var car = await uof.CarRepository.GetByIdAsync(id);

            if (car.ClientId != parsedUserId)
            {
                throw new UnauthorizedAccessException("You do not have access to this car");
            }

            await uof.CarRepository.DeleteByIdAsync(car.Id);
        }
    }
}
