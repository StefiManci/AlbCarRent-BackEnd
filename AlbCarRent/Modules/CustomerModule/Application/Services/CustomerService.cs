using AlbCarRent.Modules.CustomerModule.Application.Interfaces;
using AlbCarRent.Modules.CustomerModule.Domain;

namespace AlbCarRent.Modules.CustomerModule.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

    }
}
