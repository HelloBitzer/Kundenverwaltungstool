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
    public class FirmaController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public FirmaController(KundeDBContext context)
        {
            _context = context;
        }


        // GET: api/Firma
        [HttpGet("GetFirma")]
        public async Task<ActionResult<IEnumerable<FirmaDto>>> GetFirma()
        {
            IEnumerable<FirmaDto> firma = from p in _context.Firmas
                                          select new FirmaDto()
                                          {
                                              FirmenId = p.FirmenId,
                                              Name = p.Name,
                                              Strasse = p.Strasse,
                                              Hausnummer = p.Hausnummer,
                                              Plz = p.Plz,
                                              Ort = p.Ort
                                          };

            return Ok(firma);
        }

        // GET: api/Firma/5
        [HttpGet("GetFirma/{id}")]
        public async Task<ActionResult<FirmaDto>> GetFirma(int id)
        {
            var firma = await _context.Firmas.FindAsync(id);

            if (firma == null)
            {
                return NotFound();
            }

            var firmas = new FirmaDto()
            {
                FirmenId = firma.FirmenId,
                Name = firma.Name,
                Strasse = firma.Strasse,
                Hausnummer = firma.Hausnummer,
                Plz = firma.Plz,
                Ort = firma.Ort
            };

            return Ok(firmas);
        }



        // PUT: api/Firma/5
        [HttpPut("PutFirma/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutFirma(int id, FirmaDto firma)
        {
            if (id != firma.FirmenId)
            {
                return BadRequest();
            }

            var firmas = await _context.Firmas.FindAsync(id);
            if (firmas == null)
            {
                return NotFound();
            }

            firmas.FirmenId = firma.FirmenId;
            firmas.Name = firma.Name;
            firmas.Strasse = firma.Strasse;
            firmas.Hausnummer = firma.Hausnummer;
            firmas.Plz = firma.Plz;
            firmas.Ort = firma.Ort;


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
        [HttpPost("PostFirma")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<FirmaDto>> PostFirma(FirmaDto firma)
        {
            var firmas = new Firma()
            {
                Name = firma.Name,
                Strasse = firma.Strasse,
                Hausnummer = firma.Hausnummer,
                Plz = firma.Plz,
                Ort = firma.Ort
            };

            _context.Firmas.Add(firmas);
            await _context.SaveChangesAsync();
            firma.FirmenId = firmas.FirmenId;
            return CreatedAtAction("GetFirma", new { id = firma.FirmenId }, firma);
        }


        // DELETE: api/Firma/5
        [HttpDelete("DeleteFirma/{id}")]
        public async Task<ActionResult<FirmaDto>> DeleteFirma(int id)
        {
            var firma = await _context.Firmas.FindAsync(id);
            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmas.Remove(firma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FirmaExists(int id)
        {
            return _context.Firmas.Any(e => e.FirmenId == id);
        }
    }
}
