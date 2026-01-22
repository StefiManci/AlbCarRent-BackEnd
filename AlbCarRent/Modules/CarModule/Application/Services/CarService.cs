using AlbCarRent.Modules.CarModule.Application.Interfaces;
using AlbCarRent.Modules.CarModule.Domain;
using AlbCarRent.Modules.CarModule.DTOs;

namespace AlbCarRent.Modules.CarModule.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetCarsResponse> GetCars(GetCarsRequest request)
        {
            return await _carRepository.GetCars(request);
        }
    }
}
