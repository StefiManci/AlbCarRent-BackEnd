using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.Booking.DTOs
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public DateTime PickupDate { get; set; }
        
        public DateTime DropOffDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public int DriverAge { get; set; }

        public string AdditionalNotes { get; set; }

        public string Status { get; set; }

        public int CarId { get; set; }

        public string CarOwner { get; set; }
    }
}
