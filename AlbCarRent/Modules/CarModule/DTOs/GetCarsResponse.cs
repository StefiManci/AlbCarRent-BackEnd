using AlbCarRent.Modules.BusinessModule.DTOs;

namespace AlbCarRent.Modules.CarModule.DTOs
{
    public class GetCarsResponse
    {
        public bool Success { get; set; }

        public List<CarDto> Cars { get; set; }


        public string Message { get; set; }

        public string SingleCarImage { get; set; }
    }
}
