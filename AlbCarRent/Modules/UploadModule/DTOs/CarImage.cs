using System.ComponentModel.DataAnnotations;

namespace AlbCarRent.Modules.UploadModule.DTOs
{
    public class CarImage
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }

        public string BussinesId { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
