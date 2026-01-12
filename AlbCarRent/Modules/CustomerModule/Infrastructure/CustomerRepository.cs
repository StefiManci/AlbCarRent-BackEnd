using AlbCarRent.Datalayer;
using AlbCarRent.Modules.CustomerModule.Domain;

namespace AlbCarRent.Modules.CustomerModule.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        
    }
}
