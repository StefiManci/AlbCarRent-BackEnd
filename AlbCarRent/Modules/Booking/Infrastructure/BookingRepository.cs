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
                    CarOwner = request.CarOwner,
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

        public async Task<GetBookingsResponse> GetBookingsByBizId(string bizId,string status)
        {
            try
            {
                var bookings = await _context.Bookings.Where(b=>b.CarOwner == bizId && b.Status == status).ToListAsync();

                if (bookings.Any())
                {
                    return new GetBookingsResponse
                    {
                        Success = true,
                        Message = "Bookings returned successfully!",
                        Bookings = bookings
                    };
                }

                return new GetBookingsResponse
                {
                    Success = false,
                    Message = "You dont have any bookings with this status!",
                };
            }
            catch(Exception ex) {

                return new GetBookingsResponse
                {
                    Success = false,
                    Message = "An unexpected error has occurred!"
                };
            }
        }
    }
}
