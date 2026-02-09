using AlbCarRent.Modules.CarModule.Application.Interfaces;
using AlbCarRent.Modules.CarModule.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbCarRent.Modules.CarModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetCars([FromQuery]GetCarsRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Problem with the provided params");
                }

                var response = await _carService.GetCars(request);

                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("images/car/{carId}/business/{businessId}")]
        public async Task<IActionResult> GetCarImages(int carId,string businessId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Problem with the provided params");
                }

                var response = await _carService.GetCarImages(carId, businessId);

                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
