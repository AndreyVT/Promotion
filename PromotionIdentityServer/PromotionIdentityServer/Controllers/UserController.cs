namespace PromotionIdentityServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PromotionIdentityServer.Model;
    using PromotionIdentityServer.ViewModels;

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterUserModel registerUserModel)
        {
            ApplicationUser newUser = new ApplicationUser
            {
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
    }
}