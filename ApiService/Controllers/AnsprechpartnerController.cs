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
    public class AnsprechpartnerController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public AnsprechpartnerController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Ansprechpartner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ansprechpartner>>> GetAnsprechpartners()
        {
            return await _context.Ansprechpartners.ToListAsync();
        }

        // GET: api/Ansprechpartner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ansprechpartner>> GetAnsprechpartner(int id)
        {
            var ansprechpartner = await _context.Ansprechpartners.FindAsync(id);

            if (ansprechpartner == null)
            {
                return NotFound();
            }

            return ansprechpartner;
        }

        // PUT: api/Ansprechpartner/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnsprechpartner(int id, Ansprechpartner ansprechpartner)
        {
            if (id != ansprechpartner.AnsprechpartnerId)
            {
                return BadRequest();
            }

            _context.Entry(ansprechpartner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnsprechpartnerExists(id))
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

        // POST: api/Ansprechpartner
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ansprechpartner>> PostAnsprechpartner(Ansprechpartner ansprechpartner)
        {
            _context.Ansprechpartners.Add(ansprechpartner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnsprechpartner", new { id = ansprechpartner.AnsprechpartnerId }, ansprechpartner);
        }

        // DELETE: api/Ansprechpartner/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ansprechpartner>> DeleteAnsprechpartner(int id)
        {
            var ansprechpartner = await _context.Ansprechpartners.FindAsync(id);
            if (ansprechpartner == null)
            {
                return NotFound();
            }

            _context.Ansprechpartners.Remove(ansprechpartner);
            await _context.SaveChangesAsync();

            return ansprechpartner;
        }

        private bool AnsprechpartnerExists(int id)
        {
            return _context.Ansprechpartners.Any(e => e.AnsprechpartnerId == id);
        }
    }
}
