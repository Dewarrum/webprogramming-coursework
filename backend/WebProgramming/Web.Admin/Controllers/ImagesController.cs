using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Admin.Models.Images;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private const string MediaDomain = "http://localhost:5000";
        
        [HttpPost("Upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            var model = new UploadModel();

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Raws", fileName);
            
            await using var stream = new FileStream(directory, FileMode.CreateNew);
            await file.CopyToAsync(stream);
            
            model.Images.Add(new UploadModel.ImageInfo { Url = $"{MediaDomain}/Images/Raws/{fileName}"});
            model.Date = DateTime.UtcNow;

            return Ok(model);
        }
    }
}