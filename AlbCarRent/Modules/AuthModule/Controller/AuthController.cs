using AlbCarRent.Modules.AuthModule.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbCarRent.Modules.AuthModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    }
}
