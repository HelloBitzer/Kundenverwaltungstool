using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Context.Models;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public TerminController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Termin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Termin>>> GetTermins()
        {
            return await _context.Termins.ToListAsync();
        }

        // GET: api/Termin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Termin>> GetTermin(int id)
        {
            var termin = await _context.Termins.FindAsync(id);

            if (termin == null)
            {
                return NotFound();
            }

            return termin;
        }

        // PUT: api/Termin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermin(int id, Termin termin)
        {
            if (id != termin.TerminId)
            {
                return BadRequest();
            }

            _context.Entry(termin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminExists(id))
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

        // POST: api/Termin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Termin>> PostTermin(Termin termin)
        {
            _context.Termins.Add(termin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTermin", new { id = termin.TerminId }, termin);
        }

        // DELETE: api/Termin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Termin>> DeleteTermin(int id)
        {
            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }

            _context.Termins.Remove(termin);
            await _context.SaveChangesAsync();

            return termin;
        }

        private bool TerminExists(int id)
        {
            return _context.Termins.Any(e => e.TerminId == id);
        }
    }
}
