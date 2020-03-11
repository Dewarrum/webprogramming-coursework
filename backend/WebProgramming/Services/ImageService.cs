using System;
using System.IO;
using Hangfire;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Services
{
    public interface IImageService
    {
        Stream GetStreamForNewImage(string extension, out string path);
        Stream CropImage(Stream imageData);
        string ProcessImage(Stream stream);
    }
    
    public class FileSystemImageService : IImageService
    {
        public Stream GetStreamForNewImage(string extension, out string path)
        {
            var fileName = Guid.NewGuid().ToString("N") + extension;
            
            throw new NotImplementedException();
        }

        public string ProcessImage(Stream stream)
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