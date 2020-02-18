using System.Collections.Generic;
using Data;

namespace Web.Admin.Models.Users
{
    public class ListModel
    {
        public IEnumerable<UserInfo> UserInfos { get; set; }
        public ListSearchParams SearchParams { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalEntries { get; set; }
        public int PageNumber { get; set; }
        
        public class UserInfo
        {
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string ProfileUrl { get; set; }
        }
    }
}