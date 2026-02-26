using AlbCarRent.Modules.Booking.DTOs;
using AlbCarRent.Modules.BusinessModule.DTOs;
using AlbCarRent.Modules.BusinessModule.DTOs.Car_Rating_DTOs;
using AlbCarRent.Modules.UploadModule.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlbCarRent.Datalayer
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string> { 
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarRating> CarRatings { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        public DbSet<Booking> Bookings { get; set; }

    }

}
