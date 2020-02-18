using Data;
using Domain;
using Services.Utils;

namespace Services
{
    public interface IUserService
    {
        void Save(User user);
    }
    
    public class UserService : IUserService
    {
        private IUsersRepository UsersRepository { get; }
        private IPasswordHashingService PasswordHashingService { get; }

        public UserService(IUsersRepository usersRepository,
            IPasswordHashingService passwordHashingService)
        {
            UsersRepository = usersRepository;
            PasswordHashingService = passwordHashingService;
        }


        public void Save(User user)
        {
            user.Password = PasswordHashingService.Hash(user.Password);

            UsersRepository.Save(user);
        }
    }
}