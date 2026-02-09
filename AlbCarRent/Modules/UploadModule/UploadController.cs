using AlbCarRent.Datalayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadImage(IFormFile formFile)
        {
            try
            {
                if (formFile == null || formFile.Length == 0)
                {
                    return BadRequest("No file was uploaded");
                }

                var uploadFolder = Path.Combine(_env.WebRootPath, "images", "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var imageUrl = $"/images/uploads/{fileName}";

                return Ok(new {imageUrl});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
