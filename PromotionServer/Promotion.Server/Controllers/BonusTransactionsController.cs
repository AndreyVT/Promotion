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
    using Promotion.Entities.Classes.DataEntities;

    [Route("api/[controller]")]
    [ApiController]
    public class BonusTransactionsController : ControllerBase
    {
        private readonly PromotionDbContext _context;

        public BonusTransactionsController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/BonusTransactions
        [HttpGet]
        public IEnumerable<PBonusTransactions> GetBonusTransactions()
        {
            return _context.BonusTransactions;
        }

        // GET: api/BonusTransactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBonusTransactions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactions = await _context.BonusTransactions.FindAsync(id);

            if (pBonusTransactions == null)
            {
                return NotFound();
            }

            return Ok(pBonusTransactions);
        }

        // PUT: api/BonusTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPBonusTransactions([FromRoute] int id, [FromBody] PBonusTransactions pBonusTransactions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pBonusTransactions.Id)
            {
                return BadRequest();
            }

            _context.Entry(pBonusTransactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PBonusTransactionsExists(id))
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

        // POST: api/BonusTransactions
        [HttpPost]
        public async Task<IActionResult> PostPBonusTransactions([FromBody] PBonusTransactions pBonusTransactions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusTransactions.Add(pBonusTransactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPBonusTransactions", new { id = pBonusTransactions.Id }, pBonusTransactions);
        }

        // DELETE: api/BonusTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBonusTransactions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactions = await _context.BonusTransactions.FindAsync(id);
            if (pBonusTransactions == null)
            {
                return NotFound();
            }

            _context.BonusTransactions.Remove(pBonusTransactions);
            await _context.SaveChangesAsync();

            return Ok(pBonusTransactions);
        }

        private bool PBonusTransactionsExists(int id)
        {
            return _context.BonusTransactions.Any(e => e.Id == id);
        }
    }
}