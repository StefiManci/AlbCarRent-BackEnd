namespace AlbCarRent.Modules.BusinessModule.DTOs
{
    public class GetCarByIdResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Car Car { get; set; }
    }
}
