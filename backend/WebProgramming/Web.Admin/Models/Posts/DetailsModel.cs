using System.Collections.Generic;

namespace Web.Admin.Models.Posts
{
    public class DetailsModel
    {
        public IReadOnlyCollection<ImageInfo> Images { get; set; }
        
        public class ImageInfo
        {
            public string Url { get; set; }
        }

        public class OwnerInfo
        {
            public string Login { get; set; }
            public string DisplayName { get; set; }
        }
    }
}