using AlbCarRent.Modules.CarModule.DTOs;

namespace AlbCarRent.Modules.CarModule.Domain
{
    public interface ICarRepository
    {
        Task<GetCarsResponse> GetCars(GetCarsRequest request);
    }
}
