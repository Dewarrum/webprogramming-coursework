﻿using System.Linq;
using Domain;

namespace Data
{
    public interface IPostsRepository : IEntityRepository<Post>
    {
        IQueryable<Post> GetPostsByUserId(int userId);
    }
    
    public class PostsRepository : EntityRepositoryBase<Post>, IPostsRepository
    {
        public PostsRepository(CwContext dbContext) : base(dbContext)
        {
            
        }

        public IQueryable<Post> GetPostsByUserId(int userId)
        {
            return GetMany(p => p.OwnerId == userId);
        }
    }
}