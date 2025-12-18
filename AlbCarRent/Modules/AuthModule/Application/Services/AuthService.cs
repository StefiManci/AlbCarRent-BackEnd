using AlbCarRent.Modules.AuthModule.Application.Interfaces;
using AlbCarRent.Modules.AuthModule.Domain;

namespace AlbCarRent.Modules.AuthModule.Application.Services
{
    public class AuthService : IAuthService
    {
        public IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
    }
}
