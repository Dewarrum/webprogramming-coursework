using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Admin.Models.Images;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private const string MediaDomain = "http://localhost:5000";
        
        private ILogger<ImagesController> Logger { get; }
        private IMediaRepository MediaRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ImagesController(ILogger<ImagesController> logger,
            IMediaRepository mediaRepository,
            IUnitOfWork unitOfWork)
        {
            Logger = logger;
            MediaRepository = mediaRepository;
            UnitOfWork = unitOfWork;
        }
        
        [HttpPost("Upload")]
        public async Task<ActionResult> Upload([FromForm(Name = "images")] IFormFileCollection files)
        {
            var model = new UploadResponseModel();
            
            Logger.LogInformation($"Got {files.Count} files.");
            
            foreach (var file in files)
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                Logger.LogInformation($"File got {fileName} name.");
                
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Raws", fileName);
            
                await using var stream = new FileStream(directory, FileMode.CreateNew);
                await file.CopyToAsync(stream);
            
                model.Images.Add(new UploadResponseModel.ImageInfo { Url = $"{MediaDomain}/Images/Raws/{fileName}"});

                var buffer = new byte[file.Length];
                await using var s = new MemoryStream(buffer);
                await file.CopyToAsync(s);

                var media = new Media
                {
                    Content = buffer,
                    Type = MediaType.Image
                };
                
                MediaRepository.Save(media);
                UnitOfWork.Commit();
            }
            
            model.Date = DateTime.UtcNow;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetImage(int id)
        {
            var content = MediaRepository.GetById(id, m => m.Content);
            await using var stream = new MemoryStream(content);

            return File(content, "image/jpg");
        }
    }
}