using System.Collections.Generic;

namespace Web.Admin.Models.Users
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }

        public IEnumerable<UserInfo> Followers { get; set; }
        public IEnumerable<UserInfo> Followed { get; set; }

        public class UserInfo
        {
            public int Id { get; set; }
            public string DisplayName { get; set; }
            public string Login { get; set; }
        }
    }
}