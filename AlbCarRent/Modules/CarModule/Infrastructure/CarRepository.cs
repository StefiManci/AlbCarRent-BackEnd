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

                return new GetCarsResponse
                {
                    Success = true,
                    Message = "Cars Returned Successfully!",
                    Cars = cars
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
    }
}
