using AlbCarRent.Modules.BusinessModule.Application.Interfaces;
using AlbCarRent.Modules.BusinessModule.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlbCarRent.Modules.BusinessModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost("add-car")]
        public async Task<IActionResult> AddCar(AddCarRequest addCarRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _businessService.AddCar(addCarRequest);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-cars/{ownerId}")]
        public async Task<IActionResult> GetAllCars(string ownerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _businessService.GetAllCars(ownerId);

                if (response.Success)
                {
                    return Ok(response);
                }

                return StatusCode(500, response.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
