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
    using Promotion.Entities.Dictionary;

    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        private readonly PromotionDbContext _context;

        public BonusController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/PBonus
        [HttpGet]
        public IEnumerable<PBonus> GetBonus()
        {
            return _context.Bonus;
        }

        // GET: api/PBonus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBonus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonus = await _context.Bonus.FindAsync(id);

            if (pBonus == null)
            {
                return NotFound();
            }

            return Ok(pBonus);
        }

        // PUT: api/PBonus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPBonus([FromRoute] int id, [FromBody] PBonus pBonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pBonus.Id)
            {
                return BadRequest();
            }

            _context.Entry(pBonus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PBonusExists(id))
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

        // POST: api/PBonus
        [HttpPost]
        public async Task<IActionResult> PostPBonus([FromBody] PBonus pBonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bonus.Add(pBonus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPBonus", new { id = pBonus.Id }, pBonus);
        }

        // DELETE: api/PBonus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBonus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonus = await _context.Bonus.FindAsync(id);
            if (pBonus == null)
            {
                return NotFound();
            }

            _context.Bonus.Remove(pBonus);
            await _context.SaveChangesAsync();

            return Ok(pBonus);
        }

        private bool PBonusExists(int id)
        {
            return _context.Bonus.Any(e => e.Id == id);
        }
    }
}