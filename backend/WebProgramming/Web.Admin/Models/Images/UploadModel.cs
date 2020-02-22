using System;
using System.Collections.Generic;

namespace Web.Admin.Models.Images
{
    public class UploadModel
    {
        public int Count => Images.Count;
        public DateTime Date { get; set; }

        public IList<ImageInfo> Images { get; private set; }

        public UploadModel()
        {
            Images = new List<ImageInfo>();
        }

        public class ImageInfo
        {
            public string Url { get; set; }
        }
    }
}