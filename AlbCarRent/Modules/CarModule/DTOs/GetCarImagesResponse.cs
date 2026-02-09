namespace AlbCarRent.Modules.CarModule.DTOs
{
    public class GetCarImagesResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
