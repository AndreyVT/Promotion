namespace Promotion.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Promotion.Common.DomainEntities;
    using Promotion.Common.Interfaces;
    using Promotion.DataBase;
    using Promotion.Entities.Dictionary;
    using Promotion.Server.Base;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : PBaseController
    {
        private readonly PromotionDbContext _context;

        public RolesController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/PRoles
        [HttpGet]
        public IEnumerable<PRole> GetRole()
        {
            return _context.Role;
        }

        // GET: api/PRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pRole = await _context.Role.FindAsync(id);

            if (pRole == null)
            {
                return NotFound();
            }

            return Ok(pRole);
        }

        // PUT: api/PRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPRole([FromRoute] int id, [FromBody] PRole pRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(pRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRoleExists(id))
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

        // POST: api/PRoles
        [HttpPost]
        public async Task<IActionResult> PostPRole([FromBody] PRole pRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Role.Add(pRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPRole", new { id = pRole.Id }, pRole);
        }

        // DELETE: api/PRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pRole = await _context.Role.FindAsync(id);
            if (pRole == null)
            {
                return NotFound();
            }

            _context.Role.Remove(pRole);
            await _context.SaveChangesAsync();

            return Ok(pRole);
        }

        private bool PRoleExists(int id)
        {
            return _context.Role.Any(e => e.Id == id);
        }
    }
}