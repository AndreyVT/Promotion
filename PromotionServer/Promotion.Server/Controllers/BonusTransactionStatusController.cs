namespace Promotion.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Promotion.DataBase;
    using Promotion.Domain.Entities;
    using Promotion.Server.Base;

    [Route("api/[controller]")]
    [ApiController]
    public class BonusTransactionStatusController : PBaseController
    {
        private readonly PromotionDbContext _context;

        public BonusTransactionStatusController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/BonusTransactionStatus
        [HttpGet]
        public IEnumerable<PBonusTransactionStatus> GetBonusTransactionStatus()
        {
            return _context.BonusTransactionStatus;
        }

        // GET: api/BonusTransactionStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBonusTransactionStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactionStatus = await _context.BonusTransactionStatus.FindAsync(id);

            if (pBonusTransactionStatus == null)
            {
                return NotFound();
            }

            return Ok(pBonusTransactionStatus);
        }

        // PUT: api/BonusTransactionStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPBonusTransactionStatus([FromRoute] int id, [FromBody] PBonusTransactionStatus pBonusTransactionStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pBonusTransactionStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(pBonusTransactionStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PBonusTransactionStatusExists(id))
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

        // POST: api/BonusTransactionStatus
        [HttpPost]
        public async Task<IActionResult> PostPBonusTransactionStatus([FromBody] PBonusTransactionStatus pBonusTransactionStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusTransactionStatus.Add(pBonusTransactionStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPBonusTransactionStatus", new { id = pBonusTransactionStatus.Id }, pBonusTransactionStatus);
        }

        // DELETE: api/BonusTransactionStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBonusTransactionStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactionStatus = await _context.BonusTransactionStatus.FindAsync(id);
            if (pBonusTransactionStatus == null)
            {
                return NotFound();
            }

            _context.BonusTransactionStatus.Remove(pBonusTransactionStatus);
            await _context.SaveChangesAsync();

            return Ok(pBonusTransactionStatus);
        }

        private bool PBonusTransactionStatusExists(int id)
        {
            return _context.BonusTransactionStatus.Any(e => e.Id == id);
        }
    }
}