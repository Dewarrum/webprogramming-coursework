using Data;
using Domain;

namespace Services
{
    public interface IPostService
    {
        void Save(Post post);
    }
    
    public class PostService : IPostService
    {
        private IPostsRepository PostsRepository { get; }

        public PostService(IPostsRepository postsRepository)
        {
            PostsRepository = postsRepository;
        }
        
        public void Save(Post post)
        {
            PostsRepository.Save(post);
        }
    }
}