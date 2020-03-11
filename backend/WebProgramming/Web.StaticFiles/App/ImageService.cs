using System;
using System.IO;
using Domain;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Web.StaticFiles.App
{
    public interface IImageService
    {
        void Save(Media media);
        Stream CropImage(Stream imageData);
    }
    
    public class FileSystemImageService : IImageService
    {
        public void Save(Media media)
        {
            var fileName = Guid.NewGuid().ToString("N") + ".jpeg";
            var wwwrootImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            var directory300x400 = Path.Combine(wwwrootImages, "300x400");
            var directoryRaws = Path.Combine(wwwrootImages, "Raws");
            var directory150x150 = Path.Combine(wwwrootImages, "150x150");
        }

        public string ProcessImages()
        {
            throw new NotImplementedException();
        }

        public Stream CropImage(Stream imageData)
        {
            var image = Image.Load(imageData);
            var newWidth = 300;
            var newHeight = 400;
            var ratio = (double)image.Width / image.Height;

            var coefficient = ratio > 1 ? (double) image.Height / newHeight : (double) image.Width / newWidth;
            var offsetX = (int)Math.Round(ratio > 1 ? (ratio * newHeight) * 0.5 - newWidth * 0.5 : 0);
            var offsetY = (int)Math.Round(ratio > 1 ? 0 : newWidth * 0.5 / ratio - newHeight * 0.5);

            var resizedWidth = (int)Math.Round(image.Width / coefficient);
            var resizedHeight = (int) Math.Round(image.Height / coefficient);
            image.Mutate(i => i
                .Resize(resizedWidth, resizedHeight)
                .Crop(new Rectangle(offsetX, offsetY, newWidth, newHeight)));
            
            var stream = new MemoryStream();
            image.Save(stream, new PngEncoder());
            stream.Position = 0;

            return stream;
        }
    }
}