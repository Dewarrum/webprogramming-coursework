using Data;
using Domain;

namespace Services
{
    public interface IMediaService
    {
        void ChangeState(int id, MediaState newState);
    }
    
    public class MediaService : IMediaService
    {
        private IMediaRepository MediaRepository { get; }

        public MediaService(IMediaRepository mediaRepository)
        {
            MediaRepository = mediaRepository;
        }
        
        public void ChangeState(int id, MediaState newState)
        {
            var media = MediaRepository.GetById(id);
            media.State = newState;
            MediaRepository.Save(media);
        }
    }
}