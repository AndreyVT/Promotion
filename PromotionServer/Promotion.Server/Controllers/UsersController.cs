﻿namespace Promotion.Server.Controllers
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
    using Promotion.Common.DomainEntities;
    using Promotion.Common.Interfaces;
    using Promotion.DataBase;
    using Promotion.Entities.Busines;
    using Promotion.Entities.Dictionary;
    using Promotion.Server.Base;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : PBaseController
    {
        private readonly PromotionDbContext _context;
        private readonly IUserSynchronizer _userSynchronizer;

        public UsersController(PromotionDbContext context, IUserSynchronizer userSynchronizer)
        {
            _context = context;
            _userSynchronizer = userSynchronizer;
        }

        // GET: api/PUsers
        [HttpGet]
        public IEnumerable<PUser> GetUsers()
        {
            _userSynchronizer.SyncUsers();

            return _context.Users;
        }

        // GET: api/PUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPUser([FromRoute] int id)
        {
            _userSynchronizer.SyncUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pUser = await _context.Users.FindAsync(id);

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(pUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            _context.Users.Add(pUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPUser", new { id = pUser.Id }, pUser);
        }

        // DELETE: api/PUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pUser = await _context.Users.FindAsync(id);
            if (pUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(pUser);
            await _context.SaveChangesAsync();

            return Ok(pUser);
        }

        [HttpGet("{userLogin}/settings")]
        public async Task<IActionResult> GetUserSettings([FromRoute]string userLogin)
        {
            IdentityServerUserInfo userInfo = await _userSynchronizer.GetUserInfo(userLogin);

            var user = _context.Users.Where(c => c.ExternalId == userInfo.id).FirstOrDefault();
            if (user == null)
            {
                user = new PUser
                {
                    ExternalId = userInfo.id,
                    EMail = userInfo.email
                };

                user = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                if (_context.Users.Count() == 1)
                {
                    AddUser(user, Roles.Admin.LogicalName);
                }
                else
                {
                    if (_context.Users.Count() == 2)
                    {
                        AddUser(user, Roles.Manager.LogicalName);
                    }
                    else
                    {
                        AddUser(user, Roles.User.LogicalName);
                    }
                }
                _context.SaveChanges();
            }

            return Ok(userInfo);
        }

        private void AddUser(PUser user, string roleLogicalName)
        {
            var role = _context.Role.Where(c => c.LogicalName == roleLogicalName).FirstOrDefault();

            if (role == null)
            {
                throw new Exception();
            }

            _context.UserRole.Add(new Entities.Classes.Links.PUserRole() { Role = role, User = user });
        }

        private bool PUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}