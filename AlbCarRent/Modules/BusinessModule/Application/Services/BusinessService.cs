using AlbCarRent.Modules.BusinessModule.Application.Interfaces;
using AlbCarRent.Modules.BusinessModule.Domain;
using AlbCarRent.Modules.BusinessModule.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AlbCarRent.Modules.BusinessModule.Application.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<AddCarResponse> AddCar(AddCarRequest request)
        {
            return await _businessRepository.AddCar(request);
        }

        public async Task<GetAllCarsResponse> GetAllCars(string ownerId)
        {
            return await _businessRepository.GetAllCars(ownerId);
        }

        public async Task<GetCarByIdResponse> GetCarById(int carId)
        {
            return await _businessRepository.GetCarById(carId);
        }

        public async Task<EditCarResponse> EditCar(UpdateCarDto car)
        {
            return await _businessRepository.EditCar(car);
        }

        public async Task<DeleteCarResponse> DeleteCar(int carId)
        {
            return await _businessRepository.DeleteCar(carId);
        }

    }
}
