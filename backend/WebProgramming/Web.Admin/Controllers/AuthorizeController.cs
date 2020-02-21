using Data;
using Microsoft.AspNetCore.Mvc;
using Services.Utils;
using Web.Admin.Models.Account;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private IUsersRepository UsersRepository { get; }
        private IPasswordHashingService PasswordHashingService { get; }

        public AuthorizeController(IUsersRepository usersRepository,
            IPasswordHashingService passwordHashingService)
        {
            UsersRepository = usersRepository;
            PasswordHashingService = passwordHashingService;
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var userInfo = UsersRepository.FirstOrDefault(u => u.Login == model.Login, u => new
            {
                u.Password,
                u.Login,
                u.Id
            });

            if (userInfo is null || PasswordHashingService.Hash(model.Password) != userInfo.Password)
                return Unauthorized("Login or password is incorrect.");

            return Ok();
        }

        [HttpGet("Check")]
        public ActionResult Check()
        {
            var token = Request.Headers["Authorize"][0].Split(" ")[1];

            return Ok(token);
        }
    }
}