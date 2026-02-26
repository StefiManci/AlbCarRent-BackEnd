namespace AlbCarRent.Modules.Booking.DTOs
{
    public class AddBookingRequest
    {
        public DateTime PickupDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public int DriverAge { get; set; }

        public string AdditionalNotes { get; set; }

        public int CarId { get; set; }
    }
}
