namespace Domain
{
    public class Media : EntityBase
    {
        public MediaType Type { get; set; }
        public byte[] Content { get; set; }
    }

    public enum MediaType
    {
        Image = 0
    }
}