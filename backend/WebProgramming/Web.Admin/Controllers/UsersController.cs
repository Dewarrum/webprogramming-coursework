using System;
using System.Linq;
using Common;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Services;
using Web.Admin.Models.Users;
using Web.Common.Controllers;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : UsersControllerBase
    {
        private IUsersRepository UsersRepository { get; }
        private IUserService UserService { get; }
        private IUnitOfWork UnitOfWork { get; }
        private ILogger<UsersController> Logger { get; }

        public UsersController(IUsersRepository usersRepository,
            IUserService userService,
            IUnitOfWork unitOfWork,
            ILogger<UsersController> logger) :
            base(usersRepository, userService, logger, unitOfWork)
        {
            UsersRepository = usersRepository;
            UserService = userService;
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        [HttpGet("Profile/Followers")]
        public ActionResult Followers()
        {
            var userId = User.Claims.ToDictionary(c => c.Type, c => c.Value)["id"].AsInt();

            var model = UsersRepository.GetById(userId, u => new FollowersModel
            {
                Followers = u.Followings.Select(f => new UserInfo
                {
                    AvatarUrl = f.Follower.AvatarUrl,
                    DisplayName = f.Follower.DisplayName,
                    Email = f.Follower.Email,
                    Id = f.FollowerId
                })
            });

            return Ok(model);
        }

        [HttpPost("Profile/Edit")]
        public ActionResult Edit(EditModel model)
        {
            var user = UsersRepository.GetById(model.Id, false);

            if (user is null)
                return NotFound($"User with {model.Id} could not be found.");

            user.DisplayName = model.DisplayName;
            user.AvatarUrl = model.AvatarUrl;

            UserService.Save(user);
            UnitOfWork.Commit();

            return NoContent();
        }

        [HttpPost("Profile/Follow")]
        public ActionResult Follow(FollowModel model)
        {
            var userId = User.Claims.ToDictionary(c => c.Type, c => c.Value)["id"].AsInt();
            
            UserService.CreateSubscription(userId, model.UserToFollowId);
            UnitOfWork.Commit();

            return NoContent();
        }
    }
}