namespace Web.Admin.Models.Users
{
    public class CreateModel
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
    }
}