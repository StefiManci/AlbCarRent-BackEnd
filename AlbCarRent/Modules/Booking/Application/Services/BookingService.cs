using AlbCarRent.Modules.Booking.Application.Interfaces;
using AlbCarRent.Modules.Booking.Domain;
using AlbCarRent.Modules.Booking.DTOs;

namespace AlbCarRent.Modules.Booking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<AddBookingResponse> AddBooking(AddBookingRequest addBookingRequest)
        {
            return await _bookingRepository.AddBooking(addBookingRequest);
        }

        public async Task<GetBookingsResponse> GetBookingsByBizId(string bizId, string status)
        {
            return await _bookingRepository.GetBookingsByBizId(bizId, status);
        }
    }
}
