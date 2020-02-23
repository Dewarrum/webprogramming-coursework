using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Web.Admin.Models.Images
{
    public class UploadResponseModel
    {
        public int Count => Images.Count;
        public DateTime Date { get; set; }

        public IList<ImageInfo> Images { get; private set; }

        public UploadResponseModel()
        {
            Images = new List<ImageInfo>();
        }

        public class ImageInfo
        {
            public string Url { get; set; }
        }
    }

    public class UploadRequestModel
    {
        public IList<IFormFile> Images { get; set; }
    }
}