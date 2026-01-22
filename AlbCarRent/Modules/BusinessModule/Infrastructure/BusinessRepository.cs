using AlbCarRent.Datalayer;
using AlbCarRent.Modules.BusinessModule.Domain;
using AlbCarRent.Modules.BusinessModule.DTOs;
using AlbCarRent.Modules.CarModule.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<GetAllCarsResponse> GetAllCars(string ownerId)
        {
            try
            {
                var cars = await _dbContext.Cars.Where(c=>c.OwnedBy == ownerId).ToListAsync();

                if (cars.Any())
                {
                    return new GetAllCarsResponse
                    {
                        Success = true,
                        Message = "Cars Returned Successfully!",
                        Cars = cars
                    };
                }


                return new GetAllCarsResponse
                {
                    Success = true,
                    Message = "There are no registered cars for this owner!",
                };

            }
            catch(Exception ex)
            {
                return new GetAllCarsResponse
                {
                    Success = false,
                    Message = "Unexpected Error Occured!"
                };
            }
        }

        public async Task<GetCarByIdResponse> GetCarById(int carId)
        {
            try
            {
                var car = await _dbContext.Cars.FirstOrDefaultAsync(c=>c.Id == carId);

                if (car != null)
                {
                    return new GetCarByIdResponse
                    {
                        Success = true,
                        Message = "Car Returned Successfully",
                        Car = car
                    };
                }

                return new GetCarByIdResponse
                {
                    Success = false,
                    Message = "The car with the provided id was not found!",
                };

            }
            catch (Exception ex)
            {
                return new GetCarByIdResponse
                {
                    Success = false,
                    Message = "Unexpected Error Occured! " +ex.Message,
                };
            }
        }

        public async Task<EditCarResponse> EditCar(UpdateCarDto car)
        {
            try
            {
                var existingCar = await _dbContext.Cars.FirstOrDefaultAsync(c=>c.Id==car.Id);

                if(existingCar != null)
                {
                    if (!string.IsNullOrWhiteSpace(car.Make) && existingCar.Make != car.Make)
                        existingCar.Make = car.Make;

                    if (!string.IsNullOrWhiteSpace(car.Model) && existingCar.Model != car.Model)
                        existingCar.Model = car.Model;

                    if (!string.IsNullOrWhiteSpace(car.Color) && existingCar.Color != car.Color)
                        existingCar.Color = car.Color;

                    if (!string.IsNullOrWhiteSpace(car.LicensePlate) && existingCar.LicensePlate != car.LicensePlate)
                        existingCar.LicensePlate = car.LicensePlate;

                    if (!string.IsNullOrWhiteSpace(car.Description) && existingCar.Description != car.Description)
                        existingCar.Description = car.Description;

                    if (!string.IsNullOrWhiteSpace(car.Transmission) && existingCar.Transmission != car.Transmission)
                        existingCar.Transmission = car.Transmission;

                    if (!string.IsNullOrWhiteSpace(car.FuelType) && existingCar.FuelType != car.FuelType)
                        existingCar.FuelType = car.FuelType;

                    if (!string.IsNullOrWhiteSpace(car.OwnedBy) && existingCar.OwnedBy != car.OwnedBy)
                        existingCar.OwnedBy = car.OwnedBy;

                    if (!string.IsNullOrWhiteSpace(car.RentedBy) && existingCar.RentedBy != car.RentedBy)
                        existingCar.RentedBy = car.RentedBy;

                    if (car.Year > 0 && existingCar.Year != car.Year)
                        existingCar.Year = car.Year;

                    if (car.Mileage >= 0 && existingCar.Mileage != car.Mileage)
                        existingCar.Mileage = car.Mileage;

                    if (car.DailyRentalPrice > 0 && existingCar.DailyRentalPrice != car.DailyRentalPrice)
                        existingCar.DailyRentalPrice = car.DailyRentalPrice;

                    if (existingCar.IsAvailable != car.IsAvailable)
                        existingCar.IsAvailable = car.IsAvailable;

                    existingCar.UpdatedAt = DateTime.UtcNow;

                    await _dbContext.SaveChangesAsync();

                    return new EditCarResponse
                    {
                        Success = true,
                        Message = "Car Edited Successfully!"
                    };
                }

                return new EditCarResponse
                {
                    Success = false,
                    Message = "Car with the provided Id was not found!"
                };
            }
            catch (Exception ex)
            {
                return new EditCarResponse
                {
                    Success = false,
                    Message = "Database Error Occured! " + ex.Message,
                };
            }
        }

        public async Task<DeleteCarResponse> DeleteCar(int carId)
        {
            try
            {

                var existingCar = await _dbContext.Cars.FirstOrDefaultAsync(c=>c.Id == carId);

                if (existingCar != null)
                {
                    _dbContext.Cars.Remove(existingCar);

                    await _dbContext.SaveChangesAsync();

                    return new DeleteCarResponse
                    {
                        Success = true,
                        Message = "Car Deleted Successfully",
                    };
                }

                return new DeleteCarResponse
                {
                    Success = false,
                    Message = "Car with given Id : " + carId + " was not found",
                };

            }
            catch(Exception ex)
            {
                return new DeleteCarResponse
                {
                    Success = false,
                    Message = "Database Error Occured : " + ex.Message
                };
            }
        }
    }
}
