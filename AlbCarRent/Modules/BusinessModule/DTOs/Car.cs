using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.BusinessModule.DTOs
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; } 
        [Required]
        public string Model { get; set; } 
        [Required]
        public int Year { get; set; }
        [Required] 
        public string Color { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal DailyRentalPrice { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string Transmission { get; set; } 
        [Required]
        public string FuelType { get; set; }
        [Required]
        public int Mileage { get; set; }
        
        [Required]
        public string OwnedBy { get; set; }

        public string RentedBy { get; set; }
    }
}
