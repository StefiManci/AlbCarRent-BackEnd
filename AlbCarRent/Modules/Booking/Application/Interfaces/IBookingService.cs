using AlbCarRent.Modules.Booking.DTOs;

namespace AlbCarRent.Modules.Booking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<AddBookingResponse> AddBooking(AddBookingRequest addBookingRequest);

        Task<GetBookingsResponse> GetBookingsByBizId(string bizId, string status);

        Task<ChangeBookingStatusResponse> ChangeBookingStatus(int bookingId, string status);
    }
}
