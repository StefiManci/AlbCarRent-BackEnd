using AlbCarRent.Datalayer;
using AlbCarRent.Modules.AuthModule.Domain;

namespace AlbCarRent.Modules.AuthModule.Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
