namespace Web.Admin.Models.Users
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }
}