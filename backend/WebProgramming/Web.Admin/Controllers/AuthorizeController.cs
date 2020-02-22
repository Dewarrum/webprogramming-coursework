using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Common;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Utils;
using Web.Admin.App;
using Web.Admin.Models.Account;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private IUsersRepository UsersRepository { get; }
        private IPasswordHashingService PasswordHashingService { get; }
        private ILogger<AuthorizeController> Logger { get; }

        public AuthorizeController(IUsersRepository usersRepository,
            IPasswordHashingService passwordHashingService,
            ILogger<AuthorizeController> logger)
        {
            UsersRepository = usersRepository;
            PasswordHashingService = passwordHashingService;
            Logger = logger;
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

            var token = JwtHelpers.GenerateToken(new[]
            {
                new Claim("login", userInfo.Login),
                new Claim("id", userInfo.Id.ToString())
            }, DateTime.UtcNow.AddMinutes(30));

            return Ok(new { Token = token, model.ReturnUrl });
        }

        [HttpGet("Check"), Authorize]
        public ActionResult Check()
        {
            return NoContent();
        }
    }
}