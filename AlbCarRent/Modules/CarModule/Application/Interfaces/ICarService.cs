using AlbCarRent.Modules.CarModule.DTOs;

namespace AlbCarRent.Modules.CarModule.Application.Interfaces
{
    public interface ICarService
    {
        Task<GetCarsResponse> GetCars(GetCarsRequest request);
    }
}
