namespace PromotionIdentityServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PromotionIdentityServer.Model;
    using PromotionIdentityServer.ViewModels;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterUserModel registerUserModel)
        {
            ApplicationUser newUser = new ApplicationUser
            {
                Id = registerUserModel.Login,
                Email = registerUserModel.EMail,
                UserName = registerUserModel.Login,
                NormalizedUserName = registerUserModel.FullName,
            };

            var registerResult = _userManager.CreateAsync(newUser, registerUserModel.Password);
            if (!registerResult.Result.Succeeded)
            {
                return new BadRequestObjectResult(registerResult.Result.Errors);
            }

            return new OkObjectResult(newUser);
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<ApplicationUser> GetById(string userId)
        {
            return _userManager.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getByLogin/{login}")]
        public ActionResult<ApplicationUser> GetByLogin([FromRoute]string login)
        {
            ApplicationUser user = _userManager.Users.Where(c => c.UserName == login).FirstOrDefault();
            return user;
        }
    }
}