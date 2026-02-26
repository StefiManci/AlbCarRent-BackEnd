using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.Booking.DTOs
{
    public class AddBookingRequest
    {
        [Required(ErrorMessage = "Pickup date is required")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage ="Drop off date is required")]
        public DateTime DropOffDate { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer email is required")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Customer phone number is required")]
        [Phone]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Driver`s age is required")]
        public int DriverAge { get; set; }

        public string AdditionalNotes { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string CarOwner { get; set; }
    }
}
