using AlbCarRent.Modules.AuthModule.DTOs;

namespace AlbCarRent.Modules.AuthModule.Application.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(string userId, string userEmail, string[] roles);

        Task<LoginResponse> Login(LoginRequest request);

        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
