using AlbCarRent.Datalayer;
using AlbCarRent.Modules.BusinessModule.Domain;
using AlbCarRent.Modules.BusinessModule.DTOs;

namespace AlbCarRent.Modules.BusinessModule.Infrastructure
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BusinessRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddCarResponse> AddCar(AddCarRequest request)
        {
            try
            {
                var newCar = new Car
                {
                    Make = request.Make,
                    Model = request.Model,
                    Year = request.Year,
                    Color = request.Color,
                    LicensePlate = request.LicensePlate,
                    Description = request.Description,
                    DailyRentalPrice = request.DailyRentalPrice,
                    IsAvailable = request.IsAvailable,
                    CreatedAt = DateTime.Now,
                    Transmission = request.Transmission,
                    FuelType = request.FuelType,
                    Mileage = request.Mileage,
                    OwnedBy = request.OwnedBy,
                    RentedBy = ""
                };
                
                _dbContext.Cars.Add(newCar);
                await _dbContext.SaveChangesAsync();

                return new AddCarResponse
                {
                    Success = true,
                    Message = "Car was added successfully!"
                };

            }catch (Exception ex)
            {
                return new AddCarResponse
                {
                    Success = false,
                    Message = "Failed to add car!",
                };
            }
        }
    }
}
