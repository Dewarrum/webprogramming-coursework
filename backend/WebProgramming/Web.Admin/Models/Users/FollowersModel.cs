using System.Collections.Generic;

namespace Web.Admin.Models.Users
{
    public class FollowersModel
    {
        public IEnumerable<UserInfo> Followers { get; set; }
    }
}