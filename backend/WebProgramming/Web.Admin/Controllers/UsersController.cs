using System;
using System.Linq;
using Common;
using Data;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.Admin.Models.Users;

namespace Web.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersRepository UsersRepository { get; }
        private IUserService UserService { get; }
        private IUnitOfWork UnitOfWork { get; }

        public UsersController(IUsersRepository usersRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            UsersRepository = usersRepository;
            UserService = userService;
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

            var userInfos = UsersRepository.Search(searchParams).Select(u => new ListModel.UserInfo
            {
                Email = u.Email,
                DisplayName = u.DisplayName
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
    }
}