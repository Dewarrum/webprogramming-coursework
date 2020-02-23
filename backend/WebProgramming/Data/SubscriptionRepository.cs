using System.Linq;
using Domain;

namespace Data
{
    public interface ISubscriptionRepository : IEntityRepository<Subscription>
    {
        
    }
    public class SubscriptionRepository : EntityRepositoryBase<Subscription>, ISubscriptionRepository
    {
        protected SubscriptionRepository(CwContext dbContext) : base(dbContext)
        {
        }
    }
}