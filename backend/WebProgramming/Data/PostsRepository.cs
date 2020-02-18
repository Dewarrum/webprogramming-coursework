using Domain;

namespace Data
{
    public interface IPostsRepository : IEntityRepository<Post>
    {
        
    }
    
    public class PostsRepository : EntityRepositoryBase<Post>, IPostsRepository
    {
        public PostsRepository(CwContext dbContext) : base(dbContext)
        {
            
        }
    }
}