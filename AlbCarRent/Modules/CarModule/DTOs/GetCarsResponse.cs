using AlbCarRent.Modules.BusinessModule.DTOs;

namespace AlbCarRent.Modules.CarModule.DTOs
{
    public class GetCarsResponse
    {
        public bool Success { get; set; }

        public List<Car> Cars { get; set; }


        public string Message { get; set; }
    }
}
