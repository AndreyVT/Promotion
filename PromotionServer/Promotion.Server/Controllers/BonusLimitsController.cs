namespace Promotion.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Promotion.DataBase;
    using Promotion.Entities.Busines;
    using Promotion.Server.Base;

    [Route("api/[controller]")]
    [ApiController]
    public class BonusLimitsController : PBaseController
    {
        private readonly PromotionDbContext _context;

        public BonusLimitsController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/BonusLimits
        [HttpGet]
        public IEnumerable<PBonusLimit> GetBonusLimits()
        {
            return _context.BonusLimits;
        }

        // GET: api/BonusLimits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBonusLimit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusLimit = await _context.BonusLimits.FindAsync(id);

            if (pBonusLimit == null)
            {
                return NotFound();
            }

            return Ok(pBonusLimit);
        }

        // PUT: api/BonusLimits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPBonusLimit([FromRoute] int id, [FromBody] PBonusLimit pBonusLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pBonusLimit.Id)
            {
                return BadRequest();
            }

            _context.Entry(pBonusLimit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PBonusLimitExists(id))
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

        // POST: api/BonusLimits
        [HttpPost]
        public async Task<IActionResult> PostPBonusLimit([FromBody] PBonusLimit pBonusLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusLimits.Add(pBonusLimit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPBonusLimit", new { id = pBonusLimit.Id }, pBonusLimit);
        }

        // DELETE: api/BonusLimits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBonusLimit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusLimit = await _context.BonusLimits.FindAsync(id);
            if (pBonusLimit == null)
            {
                return NotFound();
            }

            _context.BonusLimits.Remove(pBonusLimit);
            await _context.SaveChangesAsync();

            return Ok(pBonusLimit);
        }

        private bool PBonusLimitExists(int id)
        {
            return _context.BonusLimits.Any(e => e.Id == id);
        }
    }
}