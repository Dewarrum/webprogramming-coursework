using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Services
{
    public interface IImageService
    {
        Stream GetStreamForNewImage(string extension, out string path);
        Stream CropImage(Stream imageData);
    }
    
    public class FileSystemImageService : IImageService
    {
        public Stream GetStreamForNewImage(string extension, out string path)
        {
            var fileName = Guid.NewGuid().ToString("N") + extension;
            
            throw new NotImplementedException();
        }

        public Stream CropImage(Stream imageData)
        {
            var image = Image.Load(imageData);
            var height = image.Height * 300 / image.Width;

            if (image.Width > image.Height)
            {
                image.Mutate(i => i.Resize(300, height));
            }

            var stream = new MemoryStream();
            image.Save(stream, new JpegEncoder());
            stream.Position = 0;

            return stream;
        }
    }
}