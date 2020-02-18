namespace Domain
{
    public class Post : EntityBase
    {
        public string ContentUrl { get; set; }
        public string Description { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}