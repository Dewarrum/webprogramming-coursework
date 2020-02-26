using System;
using System.IO;

namespace Services
{
    public interface IImageService
    {
        Stream GetStreamForNewImage(string extension, out string path);
    }
    
    public class FileSystemImageService : IImageService
    {
        public Stream GetStreamForNewImage(string extension, out string path)
        {
            var fileName = Guid.NewGuid().ToString("N") + extension;
            
            throw new NotImplementedException();
        }
    }
}