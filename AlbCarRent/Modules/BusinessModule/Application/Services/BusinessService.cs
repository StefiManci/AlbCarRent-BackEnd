using AlbCarRent.Modules.BusinessModule.Application.Interfaces;
using AlbCarRent.Modules.BusinessModule.Domain;
using AlbCarRent.Modules.BusinessModule.DTOs;

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
    }
}
