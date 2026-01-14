using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.BusinessModule.DTOs.Car_Rating_DTOs
{
    public class CarRating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Range(1,5)]
        public int Rating {  get; set; }

        public List<string> Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
