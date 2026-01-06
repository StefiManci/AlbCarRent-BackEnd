namespace AlbCarRent.Modules.BusinessModule.DTOs
{
    public class GetAllCarsResponse
    {
        public IEnumerable<Car> Cars { get; set; } = new List<Car>(); 
        public bool Success { get; set; }
        public string Message { get; set; }
    }

}
