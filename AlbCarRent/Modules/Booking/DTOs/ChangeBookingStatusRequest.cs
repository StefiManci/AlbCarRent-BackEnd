namespace AlbCarRent.Modules.Booking.DTOs
{
    public class ChangeBookingStatusRequest
    {
        public string Status { get; set; }
        public int BookingId { get; set; }
    }
}
