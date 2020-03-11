using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Web.StaticFiles.Models.Images;

namespace Web.StaticFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private const string Domain = "http://localhost:7000";
        
        private ILogger<ImagesController> Logger { get; }
        private IImageService ImageService { get; }

        public ImagesController(ILogger<ImagesController> logger, IImageService imageService)
        {
            Logger = logger;
            ImageService = imageService;
        }
        
        [HttpPost("Upload")]
        public async Task<ActionResult> Upload([FromForm(Name = "images")] IFormFileCollection files)
        {
            var model = new UploadModel();

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                var directory = Path.Combine("wwwroot", "Images", "Raws", fileName);

                await using var stream = System.IO.File.Create(directory);
                await file.CopyToAsync(stream);
                
                model.Images.Add(new UploadModel.ImageInfo{Url = $"{Domain}/Images/Raws/{fileName}"});
            }
            
            model.Date = DateTime.UtcNow;
            var fileNames = model.Images.Select(i => i.Url).ToArray();
            Logger.LogInformation("New urls: {@fileNames}", (object)fileNames);

            return Ok(model);
        }

        [HttpPost("Crop")]
        public async Task<ActionResult> Crop([FromForm(Name = "images")] IFormFile file)
        {
            var inputStream = file.OpenReadStream();
            var resultStream = ImageService.CropImage(inputStream);
            resultStream.Position = 0;
            return File(resultStream, "image/jpeg");
        }
    }
}