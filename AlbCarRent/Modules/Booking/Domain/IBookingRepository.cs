using AlbCarRent.Modules.Booking.DTOs;

namespace AlbCarRent.Modules.Booking.Domain
{
    public interface IBookingRepository
    {
        Task<AddBookingResponse> AddBooking(AddBookingRequest addBookingRequest);
    }
}
