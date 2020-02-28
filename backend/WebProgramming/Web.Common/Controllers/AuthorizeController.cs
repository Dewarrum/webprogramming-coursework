using System;
using System.Security.Claims;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Utils;
using Web.Common.App;
using Web.Common.Models.Authorize;

namespace Web.Common.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
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

            Logger.LogInformation($"User got {token} token.");
            return Ok(new { Token = token, model.ReturnUrl });
        }

        [HttpGet("Check"), Authorize]
        public ActionResult Check()
        {
            Logger.LogInformation("Successful check.");
            return Ok("Success");
        }
    }
}