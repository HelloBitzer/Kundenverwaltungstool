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
    public class KundenterminController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public KundenterminController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Kundentermin
        [HttpGet("GetKundentermin")]
        public async Task<ActionResult<IEnumerable<KundenterminDto>>> GetKundentermins()
        {
            IEnumerable<KundenterminDto> kundentermin = from p in _context.Kundentermins
                                                              select new KundenterminDto()
                                                              {
                                                                  AnsprechpartnerId = p.AnsprechpartnerId,
                                                                  TerminId = p.TerminId,
                                                              };

            return Ok(kundentermin);
        }

        // GET: api/Kundentermin/5
        [HttpGet("GetKundentermin/{id}")]
        public async Task<ActionResult<KundenterminDto>> GetKundentermin(int id)
        {
            var kundentermin = await _context.Kundentermins.FindAsync(id);

            if (kundentermin == null)
            {
                return NotFound();
            }

            var kundentermins = new KundenterminDto()
            {
                AnsprechpartnerId = kundentermin.AnsprechpartnerId,
                TerminId = kundentermin.AnsprechpartnerId,
            };

            return kundentermins;
        }

        // PUT: api/Kundentermin/5
        [HttpPut("PutKundentermin/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutKundentermin(int id, KundenterminDto kundentermins)
        {

            //ID
            if (id != kundentermins.AnsprechpartnerId)
            {
                return BadRequest();
            }

            var kundentermin = await _context.Kundentermins.FindAsync(id);
            if (kundentermin == null)
            {
                return NotFound();
            }

            //Daten
            kundentermin.AnsprechpartnerId = kundentermins.AnsprechpartnerId;
            kundentermin.TerminId = kundentermins.TerminId;

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
        [HttpPost("PostKundentermin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<KundenterminDto>> PostKundentermin(KundenterminDto kundentermins)
        {
            var kundentermin = new Kundentermin()
            {
                AnsprechpartnerId = kundentermins.AnsprechpartnerId,
                TerminId = kundentermins.TerminId,
            };


            _context.Kundentermins.Add(kundentermin);
            await _context.SaveChangesAsync();


            //ID
            return CreatedAtAction("GetKundentermin", new { id = kundentermin.AnsprechpartnerId, kundentermin.TerminId }, kundentermin);
        }

        // DELETE: api/Kundentermin/5
        [HttpDelete("DeleteKundentermin{id}")]
        public async Task<ActionResult<KundenterminDto>> DeleteKundentermin(int id)
        {
            var kundentermin = await _context.Kundentermins.FindAsync(id);
            if (kundentermin == null)
            {
                return NotFound();
            }

            _context.Kundentermins.Remove(kundentermin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //ID
        private bool KundenterminExists(int id)
        {
            return _context.Kundentermins.Any(e => e.AnsprechpartnerId == id);
        }
    }
}
