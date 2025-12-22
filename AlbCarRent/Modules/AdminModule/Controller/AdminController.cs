using AlbCarRent.Modules.AdminModule.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbCarRent.Modules.AdminModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminService _adminService ;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService ;
        }
    }
}
