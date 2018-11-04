using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Promotion.DataBase;
using Promotion.Entities.Classes.Dictionary;
using Promotion.Server.Base;

namespace Promotion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodsController : PBaseController
    {
        private readonly PromotionDbContext _context;

        public PeriodsController(PromotionDbContext context)
        {
            _context = context;
        }

        // GET: api/Periods
        [HttpGet]
        public IEnumerable<PPeriod> GetPeriod()
        {
            return _context.Period;
        }

        // GET: api/Periods/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPPeriod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pPeriod = await _context.Period.FindAsync(id);

            if (pPeriod == null)
            {
                return NotFound();
            }

            return Ok(pPeriod);
        }

        // PUT: api/Periods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPPeriod([FromRoute] int id, [FromBody] PPeriod pPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(pPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PPeriodExists(id))
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

        // POST: api/Periods
        [HttpPost]
        public async Task<IActionResult> PostPPeriod([FromBody] PPeriod pPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Period.Add(pPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPPeriod", new { id = pPeriod.Id }, pPeriod);
        }

        // DELETE: api/Periods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePPeriod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pPeriod = await _context.Period.FindAsync(id);
            if (pPeriod == null)
            {
                return NotFound();
            }

            _context.Period.Remove(pPeriod);
            await _context.SaveChangesAsync();

            return Ok(pPeriod);
        }

        private bool PPeriodExists(int id)
        {
            return _context.Period.Any(e => e.Id == id);
        }
    }
}