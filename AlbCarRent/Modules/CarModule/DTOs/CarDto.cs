using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.CarModule.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public string OwnedBy { get; set; }
        public string RentedBy { get; set; }

        public string Image {  get; set; }
    }
}
