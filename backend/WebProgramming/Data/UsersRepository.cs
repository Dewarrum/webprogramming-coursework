using System;
using System.Linq;
using System.Linq.Expressions;
using Common;
using Domain;

namespace Data
{
    public interface IUsersRepository : IEntityRepository<User>
    {
        IQueryable<User> Search(ListSearchParams searchParams);
    }
    
    public class UsersRepository : EntityRepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(CwContext dbContext) : base(dbContext)
        {
            
        }

        public IQueryable<User> Search(ListSearchParams searchParams)
        {
            var query = GetAll();

            if (!searchParams.Email.IsEmpty())
                query = query.Where(u => u.Email.Contains(searchParams.Email));

            if (!searchParams.Login.IsEmpty())
                query = query.Where(u => u.Login.Contains(searchParams.Login));

            if (!searchParams.DisplayName.IsEmpty())
                query = query.Where(u => u.DisplayName.Contains(searchParams.DisplayName));

            return query.OrderBy(u => u.CreatedAt).Skip(searchParams.Skip).Take(searchParams.Take);
        }
    }

    public class ListSearchParams
    {
        public string DisplayName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        
        public Expression<Func<User, bool>> BuildExpression()
        {
            Expression<Func<User, bool>> expression = u => true;

            if (!Email.IsEmpty())
                expression = expression.And(u => u.Email.Contains(Email));
            
            if (!Login.IsEmpty())
                expression = expression.And(u => u.Login.Contains(Login));
            
            if (!DisplayName.IsEmpty())
                expression = expression.And(u => u.DisplayName.Contains(DisplayName));

            return expression;
        }
    }
}