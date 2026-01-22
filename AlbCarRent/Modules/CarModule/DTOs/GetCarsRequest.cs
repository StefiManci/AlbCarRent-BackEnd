namespace AlbCarRent.Modules.CarModule.DTOs
{
    public class GetCarsRequest
    {
       public int Page { get; set; }
       public int PageSize { get; set; }
       public string? Search { get; set; }
       public int? LowPrice  { get; set; }
       public int? HighPrice { get; set; }
       public string? FuelType   { get; set; }
    }
}
