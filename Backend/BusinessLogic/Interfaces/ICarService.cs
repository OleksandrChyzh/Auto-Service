using BusinessLogic.Models.CarModel;
using System.Security.Claims;


namespace BusinessLogic.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<GetCar>> GetCarsAsync(ClaimsPrincipal user);

        Task<int> AddCarAsync(CreateCar dto, ClaimsPrincipal user);

        Task DeleteCarAsync(int id, ClaimsPrincipal user);


    }
}
