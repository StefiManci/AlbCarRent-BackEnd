using AlbCarRent.Datalayer;
using AlbCarRent.Migrations;
using AlbCarRent.Modules.CarModule.Domain;
using AlbCarRent.Modules.CarModule.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AlbCarRent.Modules.CarModule.Infrastructure
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public CarRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<GetCarsResponse> GetCars(GetCarsRequest request)
        {
            try
            {
                var query = _dbContext.Cars.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(c =>
                        c.Make.Contains(request.Search) ||
                        c.Model.Contains(request.Search));
                }

                if (request.LowPrice > 0)
                {
                    query = query.Where(c => c.DailyRentalPrice >= request.LowPrice);
                }

                if (request.HighPrice > 0)
                {
                    query = query.Where(c => c.DailyRentalPrice <= request.HighPrice);
                }

                if (!string.IsNullOrWhiteSpace(request.FuelType))
                {
                    query = query.Where(c => c.FuelType == request.FuelType);
                }

                var totalCount = await query.CountAsync();

                var cars = await query
                    .OrderByDescending(c => c.Id)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var carList = new List<CarDto>();

                foreach (var car in cars)
                {
                    var carImage = await GetCarImages(car.Id,car.OwnedBy);


                    var singleCar = new CarDto
                    {
                        Id = car.Id,
                        Make = car.Make,
                        Model = car.Model,
                        Year = car.Year,
                        Color = car.Color,
                        LicensePlate = car.LicensePlate,
                        Description = car.Description,
                        DailyRentalPrice = car.DailyRentalPrice,
                        IsAvailable = car.IsAvailable,
                        CreatedAt = car.CreatedAt,
                        UpdatedAt = car.UpdatedAt,
                        Transmission = car.Transmission,
                        FuelType = car.FuelType,
                        Mileage = car.Mileage,
                        OwnedBy = car.OwnedBy,
                        RentedBy = car.RentedBy,
                        Image = carImage?.ImageUrls?[0] ?? ""
                    };

                    carList.Add(singleCar);
                }

                return new GetCarsResponse
                {
                    Success = true,
                    Message = "Cars Returned Successfully!",
                    Cars = carList
                };
            }
            catch (Exception ex)
            {
                return new GetCarsResponse
                {
                    Success = false,
                    Message = "Unexpected Error Occurred!",
                };
            }
        }

        public async Task<GetCarImagesResponse> GetCarImages(int carId,string businessId)
        {
            try
            {
                var carImages = await _dbContext.CarImages.FirstOrDefaultAsync(c=>c.CarId == carId);

                if(carImages!= null)
                {
                    return new GetCarImagesResponse
                    {
                        Success = true,
                        Message = "Car Images Returned Successfully!",
                        ImageUrls = carImages.ImageUrls
                    };
                }

                return new GetCarImagesResponse
                {
                    Success = true,
                    Message = "This car has no uploaded images!",
                };
            }catch (Exception ex)
            {
                return new GetCarImagesResponse
                {
                    Success = false,
                    Message = "An unexpected error occurred!"
                };
            }
        }
    }
}
