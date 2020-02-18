using System.Collections.Generic;

namespace Domain
{
    public class User : EntityBase
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }

        public List<Post> Posts { get; set; }
        public List<User> Friends { get; set; }
    }
}