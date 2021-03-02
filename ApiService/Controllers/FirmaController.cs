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
    public class FirmaController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public FirmaController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Firma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Firma>>> GetFirmas()
        {
            return await _context.Firmas.ToListAsync();
        }

        // GET: api/Firma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Firma>> GetFirma(int id)
        {
            var firma = await _context.Firmas.FindAsync(id);

            if (firma == null)
            {
                return NotFound();
            }

            return firma;
        }

        // PUT: api/Firma/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirma(int id, Firma firma)
        {
            if (id != firma.FirmenId)
            {
                return BadRequest();
            }

            _context.Entry(firma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmaExists(id))
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

        // POST: api/Firma
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Firma>> PostFirma(Firma firma)
        {
            _context.Firmas.Add(firma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirma", new { id = firma.FirmenId }, firma);
        }

        // DELETE: api/Firma/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Firma>> DeleteFirma(int id)
        {
            var firma = await _context.Firmas.FindAsync(id);
            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmas.Remove(firma);
            await _context.SaveChangesAsync();

            return firma;
        }

        private bool FirmaExists(int id)
        {
            return _context.Firmas.Any(e => e.FirmenId == id);
        }
    }
}
