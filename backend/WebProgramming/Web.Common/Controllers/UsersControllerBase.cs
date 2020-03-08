using System;
using System.Linq;
using Common;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Web.Common.Models.Users;

namespace Web.Common.Controllers
{
    public class UsersControllerBase : ControllerBase
    {
        private IUsersRepository UsersRepository { get; }
        private IUserService UserService { get; }
        private ILogger<UsersControllerBase> Logger { get; }
        private IUnitOfWork UnitOfWork { get; }

        public UsersControllerBase(IUsersRepository usersRepository,
            IUserService userService,
            ILogger<UsersControllerBase> logger,
            IUnitOfWork unitOfWork)
        {
            UsersRepository = usersRepository;
            UserService = userService;
            Logger = logger;
            UnitOfWork = unitOfWork;
        }
        
        [HttpGet("List")]
        public ActionResult List(int skip = 0, int take = 10, string displayName = null, string login = null,
            string email = null)
        {
            var searchParams = new ListSearchParams
            {
                Skip = skip,
                Take = take,
                DisplayName = displayName,
                Login = login,
                Email = email
            };

            var userInfos = UsersRepository.Search(searchParams).Select(u => new UserInfo
            {
                Email = u.Email,
                DisplayName = u.DisplayName,
                Id = u.Id,
                AvatarUrl = u.AvatarUrl
            });

            var totalEntries = UsersRepository.Count(searchParams.BuildExpression());
            var pageNumber = skip / take;
            var totalPageCount = Math.Ceiling((decimal) totalEntries / take).AsInt();

            var model = new ListModel
            {
                UserInfos = userInfos,
                SearchParams = searchParams,
                TotalEntries = totalEntries,
                TotalPageCount = totalPageCount,
                PageNumber = pageNumber
            };

            return Ok(model);
        }

        [HttpGet("Profile/{id}")]
        public ActionResult Profile(int id)
        {
            var userInfo = UsersRepository.GetById(id, u => new ProfileModel
            {
                Login = u.Login,
                Email = u.Email,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Followers = u.Followings.Select(f => new ProfileModel.UserInfo
                {
                    DisplayName = f.Follower.DisplayName,
                    Id = f.Follower.Id,
                    Login = f.Follower.Login
                }),
                Followed = u.Followments.Select(f => new ProfileModel.UserInfo
                {
                    DisplayName = f.Followed.DisplayName,
                    Id = f.Followed.Id,
                    Login = f.Followed.Login
                })
            }, false);

            if (userInfo is null)
                return NotFound($"User with {id} id not found");

            return Ok(userInfo);
        }
        
        [HttpGet("MyProfile")]
        public ActionResult MyProfile()
        {
            var userId = User.Claims.ToDictionary(c => c.Type, c => c.Value)["id"].AsInt();

            var userInfo = UsersRepository.GetById(userId, u => new
            {
                u.Login,
                u.Email,
                u.DisplayName,
                u.AvatarUrl
            });
            
            Logger.LogInformation($"User requested profile data {userInfo.ToJson()}.");

            return Ok(userInfo);
        }
    }
}