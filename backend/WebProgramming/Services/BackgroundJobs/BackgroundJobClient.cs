using System;
using System.Linq;
using System.Linq.Expressions;
using Common;
using Data;
using Domain;
using Hangfire;

namespace Services.BackgroundJobs
{
    public interface IBackgroundJobClient
    {
        int Enqueue<TJob>(Expression<Action<TJob>> execute);
    }
    
    public class BackgroundJobClient : IBackgroundJobClient
    {
        public int Enqueue<TJob>(Expression<Action<TJob>> execute)
        {
            return BackgroundJob.Enqueue(execute).AsInt();
        }
    }

    public interface IBackgroundJob
    {
        void Execute();
    }

    public interface IBackgroundJob<in TData>
    {
        void Execute(TData data);
    }

    public class ImageProcessingJob : IBackgroundJob<string>
    {
        private IMediaRepository MediaRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ImageProcessingJob(IMediaRepository mediaRepository,
            IUnitOfWork unitOfWork)
        {
            MediaRepository = mediaRepository;
            UnitOfWork = unitOfWork;
        }
        
        public void Execute(string path)
        {
            var unprocessedImages = MediaRepository.GetMany(m => m.State == MediaState.Unprocessed).ToArray();

            foreach (var imageData in unprocessedImages)
            {
            }
        }
    }
}