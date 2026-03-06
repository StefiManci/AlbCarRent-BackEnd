using AlbCarRent.Modules.Booking.Application.Interfaces;
using AlbCarRent.Modules.Booking.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AlbCarRent.Modules.Booking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> AddBooking([FromBody] AddBookingRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid data was sent to the server");
                }

                var response = await _bookingService.AddBooking(request);

                return  Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error has occured.Try again later!");
            }
        }

        [HttpGet("bookings/{ownerId}")]
        public async Task<IActionResult> GetBookings([FromQuery] string status,string ownerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data was sent to the server");
                }

                var response = await _bookingService.GetBookingsByBizId(ownerId,status);

                return Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error has occured.Try again later!");
            }
        }

        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeBookingStatus([FromBody]ChangeBookingStatusRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data was send to the server");
                }

                return Ok(await _bookingService.ChangeBookingStatus(request.BookingId, request.Status));

            }catch(Exception e)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
    }
}
