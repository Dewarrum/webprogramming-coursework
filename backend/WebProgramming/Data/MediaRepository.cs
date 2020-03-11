using Domain;

namespace Data
{
    public interface IMediaRepository : IEntityRepository<Media>
    {
        
    }
    
    public class MediaRepository : EntityRepositoryBase<Media>, IEntityRepository<Media>
    {
        public MediaRepository(CwContext dbContext) : base(dbContext)
        {
        }
    }
}