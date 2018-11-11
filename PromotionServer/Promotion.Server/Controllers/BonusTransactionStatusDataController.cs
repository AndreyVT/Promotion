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
    public class BonusTransactionStatusDataController : PBaseController
    {
        private readonly PromotionDbContext _context;

        public BonusTransactionStatusDataController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/BonusTransactionStatusData
        [HttpGet]
        public IEnumerable<PBonusTransactionStatusData> GetBonusTransactionStatusData()
        {
            return _context.BonusTransactionStatusData;
        }

        // GET: api/BonusTransactionStatusData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBonusTransactionStatusData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactionStatusData = await _context.BonusTransactionStatusData.FindAsync(id);

            if (pBonusTransactionStatusData == null)
            {
                return NotFound();
            }

            return Ok(pBonusTransactionStatusData);
        }

        // PUT: api/BonusTransactionStatusData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPBonusTransactionStatusData([FromRoute] int id, [FromBody] PBonusTransactionStatusData pBonusTransactionStatusData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pBonusTransactionStatusData.Id)
            {
                return BadRequest();
            }

            _context.Entry(pBonusTransactionStatusData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PBonusTransactionStatusDataExists(id))
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

        // POST: api/BonusTransactionStatusData
        [HttpPost]
        public async Task<IActionResult> PostPBonusTransactionStatusData([FromBody] PBonusTransactionStatusData pBonusTransactionStatusData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusTransactionStatusData.Add(pBonusTransactionStatusData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPBonusTransactionStatusData", new { id = pBonusTransactionStatusData.Id }, pBonusTransactionStatusData);
        }

        // DELETE: api/BonusTransactionStatusData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBonusTransactionStatusData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pBonusTransactionStatusData = await _context.BonusTransactionStatusData.FindAsync(id);
            if (pBonusTransactionStatusData == null)
            {
                return NotFound();
            }

            _context.BonusTransactionStatusData.Remove(pBonusTransactionStatusData);
            await _context.SaveChangesAsync();

            return Ok(pBonusTransactionStatusData);
        }

        private bool PBonusTransactionStatusDataExists(int id)
        {
            return _context.BonusTransactionStatusData.Any(e => e.Id == id);
        }
    }
}