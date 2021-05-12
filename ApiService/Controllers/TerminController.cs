using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Context.Models;
using Dtos;

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
        [HttpGet("GetTermin")]
        public async Task<ActionResult<IEnumerable<TerminDto>>> GetTermin()
        {
            IEnumerable<TerminDto> termins = from p in _context.Termins
                                             select new TerminDto()
                                             {
                                                 TerminId = p.TerminId,
                                                 Start = p.Start,
                                                 Ende = p.Ende,
                                                 Bemerkung = p.Bemerkung
                                             };

            return Ok(termins);
        }


        // GET: api/Termin/5
        [HttpGet("GetTermin{id}")]
        public async Task<ActionResult<TerminDto>> GetTermin(int id)
        {
            var termin = await _context.Termins.FindAsync(id);

            if (termin == null)
            {
                return NotFound();
            }

            var termins = new TerminDto()
            {
                TerminId = termin.TerminId,
                Start = termin.Start,
                Ende = termin.Ende,
                Bemerkung = termin.Bemerkung
            };

            return Ok(termins);
        }

        // PUT: api/Termin/5
        [HttpPut("PutTermin/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutTermin(int id, TerminDto termins)
        {
            if (id != termins.TerminId)
            {
                return BadRequest();
            }

            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }

            //Daten
            termin.TerminId = termins.TerminId;
            termin.Start = termins.Start;
            termin.Ende = termins.Ende;
            termin.Bemerkung = termins.Bemerkung;

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
        [HttpPost("PostTermin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TerminDto>> PostTermin(TerminDto termins)
        {
            var termin = new Termin()
            {
           
                Start = termins.Start,
                Ende = termins.Ende,
                Bemerkung = termins.Bemerkung
            };


            _context.Termins.Add(termin);
            await _context.SaveChangesAsync();
            termins.TerminId = termin.TerminId;
            return CreatedAtAction("GetTermin", new { id = termins.TerminId }, termins);
        }

        // DELETE: api/Termin/5
        [HttpDelete("DeleteTermin/{id}")]
        public async Task<ActionResult<TerminDto>> DeleteTermin(int id)
        {
            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }

            _context.Termins.Remove(termin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TerminExists(int id)
        {
            return _context.Termins.Any(e => e.TerminId == id);
        }
    }
}
