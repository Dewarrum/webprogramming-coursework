namespace Data
{
    public interface IUnitOfWork
    {
        void Commit();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private CwContext DbContext { get; }

        public UnitOfWork(CwContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}