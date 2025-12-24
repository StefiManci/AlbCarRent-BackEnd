using AlbCarRent.Datalayer;
using AlbCarRent.Modules.BusinessModule.Domain;

namespace AlbCarRent.Modules.BusinessModule.Infrastructure
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BusinessRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
