using AlbCarRent.Datalayer;
using AlbCarRent.Modules.AdminModule.Domain;

namespace AlbCarRent.Modules.AdminModule.Infrastructure
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
    }
}
