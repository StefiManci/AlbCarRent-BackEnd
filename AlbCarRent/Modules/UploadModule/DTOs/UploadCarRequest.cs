namespace AlbCarRent.Modules.UploadModule.DTOs
{
    public class UploadCarRequest
    {
      public IFormFile FormFile {  get; set; }

      public int CarId { get; set; }

      public string BusinessId { get; set; }
    }
}
