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
    public class KundenterminController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public KundenterminController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Kundentermin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kundentermin>>> GetKundentermins()
        {
            return await _context.Kundentermins.ToListAsync();
        }

        // GET: api/Kundentermin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kundentermin>> GetKundentermin(int id)
        {
            var kundentermin = await _context.Kundentermins.FindAsync(id);

            if (kundentermin == null)
            {
                return NotFound();
            }

            return kundentermin;
        }

        // PUT: api/Kundentermin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKundentermin(int id, Kundentermin kundentermin)
        {
            if (id != kundentermin.KundenterminId)
            {
                return BadRequest();
            }

            _context.Entry(kundentermin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KundenterminExists(id))
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

        // POST: api/Kundentermin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kundentermin>> PostKundentermin(Kundentermin kundentermin)
        {
            _context.Kundentermins.Add(kundentermin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKundentermin", new { id = kundentermin.KundenterminId }, kundentermin);
        }

        // DELETE: api/Kundentermin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kundentermin>> DeleteKundentermin(int id)
        {
            var kundentermin = await _context.Kundentermins.FindAsync(id);
            if (kundentermin == null)
            {
                return NotFound();
            }

            _context.Kundentermins.Remove(kundentermin);
            await _context.SaveChangesAsync();

            return kundentermin;
        }

        private bool KundenterminExists(int id)
        {
            return _context.Kundentermins.Any(e => e.KundenterminId == id);
        }
    }
}
