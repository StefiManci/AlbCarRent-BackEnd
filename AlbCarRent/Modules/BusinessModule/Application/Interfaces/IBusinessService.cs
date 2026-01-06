using AlbCarRent.Modules.BusinessModule.DTOs;

namespace AlbCarRent.Modules.BusinessModule.Application.Interfaces
{
    public interface IBusinessService
    {
        Task<AddCarResponse> AddCar(AddCarRequest request);

        Task<GetAllCarsResponse> GetAllCars(string ownerId);
    }
}
