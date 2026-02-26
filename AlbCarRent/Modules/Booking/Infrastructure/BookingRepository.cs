using AlbCarRent.Datalayer;
using AlbCarRent.Modules.Booking.Domain;
using AlbCarRent.Modules.Booking.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AlbCarRent.Modules.Booking.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddBookingResponse> AddBooking(AddBookingRequest request)
        {
            try
            {
                if (request.PickupDate >= request.DropOffDate)
                {
                    return new AddBookingResponse
                    {
                        Success = false,
                        Message = "Invalid booking dates."
                    };
                }

                var conflict = await _context.Bookings
                    .FirstOrDefaultAsync(b =>
                        b.CarId == request.CarId &&
                        b.Status == "ACCEPTED" &&
                        b.PickupDate < request.DropOffDate &&
                        b.DropOffDate > request.PickupDate);

                if (conflict != null)
                {
                    return new AddBookingResponse
                    {
                        Success = false,
                        Message = "Car is already booked for selected dates."
                    };
                }

                var booking = new AlbCarRent.Modules.Booking.DTOs.Booking
                {
                    PickupDate = request.PickupDate,
                    DropOffDate = request.DropOffDate,
                    CustomerName = request.CustomerName,
                    CustomerEmail = request.CustomerEmail,
                    CustomerPhone = request.CustomerPhone,
                    DriverAge = request.DriverAge,
                    AdditionalNotes = request.AdditionalNotes,
                    Status = "PENDING",
                    CarId = request.CarId,
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return new AddBookingResponse
                {
                    Success = true,
                    Message = "Booking request was sent to the owner!"
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error"+ex.Message);
                return new AddBookingResponse
                {
                    Success = false,
                    Message = "An unexpected error occurred. Try again later!"
                };
            }
        }
    }
}
