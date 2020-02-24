﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Admin.Models.Images;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private const string MediaDomain = "http://localhost:5000";
        
        private ILogger<ImagesController> Logger { get; }

        public ImagesController(ILogger<ImagesController> logger)
        {
            Logger = logger;
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
            }
            
            model.Date = DateTime.UtcNow;

            return Ok(model);
        }

        public class Image
        {
            public string Name { get; set; }
            public int Size { get; set; }
        }
    }
}