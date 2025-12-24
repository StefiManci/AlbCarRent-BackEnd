using AlbCarRent.Modules.BusinessModule.Application.Interfaces;
using AlbCarRent.Modules.BusinessModule.Domain;

namespace AlbCarRent.Modules.BusinessModule.Application.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }
    }
}
