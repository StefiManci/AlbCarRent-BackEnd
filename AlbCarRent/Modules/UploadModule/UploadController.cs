using AlbCarRent.Datalayer;
using AlbCarRent.Modules.UploadModule.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlbCarRent.Modules.UploadModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public UploadController(IWebHostEnvironment env,ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm]UploadCarRequest request)
        {
            try
            {
                if (request.FormFile == null || request.FormFile.Length == 0)
                {
                    return BadRequest(new UploadCarResponse
                    {
                        Success = false,
                        Message = "No file was uploaded!"
                    });
                }

                var uploadFolder = Path.Combine(_env.WebRootPath, "images", "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.FormFile.FileName)}";
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.FormFile.CopyToAsync(stream);
                }
                var imageUrl = $"/images/uploads/{fileName}";

                //Check if the This car has images on db

                var existingImages = await _context.CarImages.FirstOrDefaultAsync(c=>c.CarId == request.CarId);

                if (existingImages != null)
                {
                    existingImages.ImageUrls.Add(imageUrl);
                }
                else
                {
                    var carImage = new CarImage
                    {
                        CarId = request.CarId,
                        BussinesId = request.BusinessId,
                        ImageUrls = new List<string> { imageUrl },
                    };
                    _context.CarImages.Add(carImage);
                }

                await _context.SaveChangesAsync();

                return Ok(new UploadCarResponse
                {
                    Success = true,
                    Message = "Image Uploaded Successfully!",
                    ImageUrl = imageUrl,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
