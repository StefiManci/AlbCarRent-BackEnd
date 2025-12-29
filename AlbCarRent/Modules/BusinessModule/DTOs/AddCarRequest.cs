using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.BusinessModule.DTOs
{
    public class AddCarRequest
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public string OwnedBy { get; set; }

    }
}
