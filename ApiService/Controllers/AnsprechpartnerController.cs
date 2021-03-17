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
    public class AnsprechpartnerController : ControllerBase
    {
        private readonly KundeDBContext _context;

        public AnsprechpartnerController(KundeDBContext context)
        {
            _context = context;
        }

        // GET: api/Ansprechpartner
        [HttpGet("GetAnsprechpartner")]
        public ActionResult<IEnumerable<AnsprechpartnerDto>> GetAnsprechpartner()
        {
            IEnumerable<AnsprechpartnerDto> ansprechpartner = from p in _context.Ansprechpartners
                                                              select new AnsprechpartnerDto()
                                                              {
                                                                  AnsprechpartnerId = p.AnsprechpartnerId,
                                                                  Nachname = p.Nachname,
                                                                  Vorname = p.Vorname,
                                                                  Telefon = p.Telefon,
                                                                  Email = p.Email,
                                                                  FirmenId = p.FirmenId,
                                                                  Titel = p.Titel
                                                              };
            return Ok(ansprechpartner);
        }


        // GET: api/Ansprechpartner/5
        [HttpGet("GetAnsprechpartner/{id}")]
        public async Task<ActionResult<AnsprechpartnerDto>> GetAnsprechpartner(int id)
        {
            var ansprechpartner = await _context.Ansprechpartners.FindAsync(id);

            if (ansprechpartner == null)
            {
                return NotFound();
            }

            //Fragen warum get und new

            var ansprechpartners = new AnsprechpartnerDto()
            {
                AnsprechpartnerId = ansprechpartner.AnsprechpartnerId,
                Nachname = ansprechpartner.Nachname,
                Vorname = ansprechpartner.Vorname,
                Telefon = ansprechpartner.Telefon,
                Email = ansprechpartner.Email,
                FirmenId = ansprechpartner.FirmenId,
                Titel = ansprechpartner.Titel
            };

            return Ok(ansprechpartners);
        }

        // PUT: api/Ansprechpartner/5
        [HttpPut("PutAnsprechpartner/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAnsprechpartner(int id, AnsprechpartnerDto ansprechpartner)
        {
            if (id != ansprechpartner.AnsprechpartnerId)
            {
                return BadRequest();
            }

            var ansprechpartners = await _context.Ansprechpartners.FindAsync(id);
            if (ansprechpartners == null)
            {
                return NotFound();
            }

            ansprechpartners.AnsprechpartnerId = ansprechpartner.AnsprechpartnerId;
            ansprechpartners.Nachname = ansprechpartner.Nachname;
            ansprechpartners.Vorname = ansprechpartner.Vorname;
            ansprechpartners.Telefon = ansprechpartner.Telefon;
            ansprechpartners.Email = ansprechpartner.Email;
            ansprechpartners.FirmenId = ansprechpartner.FirmenId;
            ansprechpartners.Titel = ansprechpartner.Titel;

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
        /// <summary>
        /// Erzeugt einen neuen Ansprechpartner
        /// </summary>
        /// <param name="ansprechpartner"></param>
        /// <returns></returns>
        /// <response code="409">FirmenID existiert nicht</response>
        [HttpPost("PostAnsprechpartner")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AnsprechpartnerDto>> PostAnsprechpartner(AnsprechpartnerDto ansprechpartner)
        {
            //Prüfen ob es FirmenID gibt

           var firma = _context.Firmas.FindAsync(ansprechpartner.FirmenId);
           if (firma == null)
           {
               return Conflict();
           }

            var ansprechpartners = new Ansprechpartner()
            {
                Nachname = ansprechpartner.Nachname,
                Vorname = ansprechpartner.Vorname,
                Telefon = ansprechpartner.Telefon,
                Email = ansprechpartner.Email,
                FirmenId = ansprechpartner.FirmenId,
                Titel = ansprechpartner.Titel
            };


            _context.Ansprechpartners.Add(ansprechpartners);
            await _context.SaveChangesAsync();

            ansprechpartner.AnsprechpartnerId = ansprechpartners.AnsprechpartnerId;
            
            return CreatedAtAction(nameof(GetAnsprechpartner), new { id = ansprechpartner.AnsprechpartnerId }, ansprechpartner);
        }

        // DELETE: api/Ansprechpartner/5
        [HttpDelete("DeleteAnsprechpartner/{id}")]
        public async Task<ActionResult<AnsprechpartnerDto>> DeleteAnsprechpartner(int id)
        {
            var ansprechpartner = await _context.Ansprechpartners.FindAsync(id);
            if (ansprechpartner == null)
            {
                return NotFound();
            }

            _context.Ansprechpartners.Remove(ansprechpartner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnsprechpartnerExists(int id)
        {
            return _context.Ansprechpartners.Any(e => e.AnsprechpartnerId == id);
        }
    }
}
