namespace Promotion.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Promotion.Common;
    using Promotion.Common.Dictionaries;
    using Promotion.Common.Interfaces;
    using Promotion.DataBase;
    using Promotion.Domain.Entities;
    using Promotion.Domain.Services;
    using Promotion.DomainWebLayer.Mappers;
    using Promotion.Server.Base;
    using Promotion.Server.Web.ViewModel;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : PBaseController
    {
        private readonly IUserSynchronizer _userSynchronizer;
        private readonly UserSettingsMapper _userSettingsMapper;
        private readonly UserService _userService;
        private readonly UserRolesService _userRolesService;
        private readonly RoleService _roleService;

        public UsersController(IUserSynchronizer userSynchronizer, UserSettingsMapper userSettingsMapper, UserService userService, UserRolesService userRolesService, RoleService roleService)
        {
            _userSynchronizer = userSynchronizer;
            _userSettingsMapper = userSettingsMapper;
            _userService = userService;
            _userRolesService = userRolesService;
            _roleService = roleService;
        }

        // GET: api/PUsers
        [HttpGet]
        public IEnumerable<PUser> GetUsers()
        {
            return _userService.Get();
        }

        // GET: api/PUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPUser([FromRoute] int id)
        {
            var pUser = await _userService.FindAsync(id);

            if (pUser == null)
            {
                return NotFound();
            }

            return Ok(pUser);
        }

        // PUT: api/PUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPUser([FromRoute] int id, [FromBody] PUser pUser)
        {
           await _userService.Update(pUser);

            return NoContent();
        }

        // POST: api/PUsers
        [HttpPost]
        public async Task<IActionResult> PostPUser([FromBody] PUser pUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.Create(pUser);

            return CreatedAtAction("GetPUser", new { id = pUser.Id }, pUser);
        }

        // DELETE: api/PUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePUser([FromRoute] int id)
        {
           await _userService.Remove(id);

            return Ok();
        }

        [HttpGet("{userLogin}/settings")]
        public async Task<IActionResult> GetUserSettings([FromRoute]string userLogin)
        {
            await AutoRegisterUser(userLogin);

            UserSettingsVM userSettingsVM = _userSettingsMapper.GetUserSettings(userLogin);

            return new OkObjectResult(userSettingsVM);
        }

        private void AddUserRole(PUser user, string roleLogicalName)
        {
            PRole role = _roleService.GetRoleByLogicalName(roleLogicalName);

            if (role == null)
            {
                throw new Exception();
            }

            _userRolesService.Add(new PUserRole() { Role = role, User = user });
        }

        /// <summary>
        /// Первому пользовтаелю автоматом дадим права админа
        /// Второму - менеджера
        /// Всех регистриуем в юзерах если еще нет с таким логином
        /// </summary>
        /// <param name="userLogin"></param>
        private async Task AutoRegisterUser(string userLogin)
        {
            IdentityServerUserInfo userInfo = await _userSynchronizer.GetUserInfo(userLogin);

            PUser user = _userService.GetUserByExternalId(userInfo.id);
            if (user == null)
            {
                user = new PUser
                {
                    ExternalId = userInfo.id,
                    EMail = userInfo.email
                };

                user = _userService.Create(user).Result;

                if (_userService.Get().Count() == 1)
                {
                    AddUserRole(user, Roles.Admin.LogicalName);
                }
                else
                {
                    if (_userService.Get().Count() == 2)
                    {
                        AddUserRole(user, Roles.Manager.LogicalName);
                    }
                    else
                    {
                        AddUserRole(user, Roles.User.LogicalName);
                    }
                }
            }
        }
    }
}