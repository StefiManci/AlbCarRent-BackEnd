namespace AlbCarRent.Modules.Booking.DTOs
{
    public class GetBookingsResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
