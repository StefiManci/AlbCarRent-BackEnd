using AlbCarRent.Modules.AdminModule.Application.Interfaces;
using AlbCarRent.Modules.AdminModule.Domain;

namespace AlbCarRent.Modules.AdminModule.Application.Services
{
    public class AdminService : IAdminService
    {
        public IAdminRepository _adminRepository { get; set; }

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
    }
}
