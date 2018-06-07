using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksWeb.Models;

namespace BooksWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Communications")]
    public class CommunicationsController : Controller
    {
        private readonly BookingClubContext _context;

        public CommunicationsController(BookingClubContext context)
        {
            _context = context;
        }

        // GET: api/Communications
        [HttpGet]
        public IEnumerable<Communications> GetCommunications()
        {
            return _context.Communications;
        }

        // GET: api/Communications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunications([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communications = await _context.Communications.SingleOrDefaultAsync(m => m.CommunicationId == id);

            if (communications == null)
            {
                return NotFound();
            }

            return Ok(communications);
        }

        // PUT: api/Communications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunications([FromRoute] int id, [FromBody] Communications communications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != communications.CommunicationId)
            {
                return BadRequest();
            }

            _context.Entry(communications).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationsExists(id))
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

        // POST: api/Communications
        [HttpPost]
        public async Task<IActionResult> PostCommunications([FromBody] Communications communications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Communications.Add(communications);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunications", new { id = communications.CommunicationId }, communications);
        }

        // DELETE: api/Communications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunications([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communications = await _context.Communications.SingleOrDefaultAsync(m => m.CommunicationId == id);
            if (communications == null)
            {
                return NotFound();
            }

            _context.Communications.Remove(communications);
            await _context.SaveChangesAsync();

            return Ok(communications);
        }

        private bool CommunicationsExists(int id)
        {
            return _context.Communications.Any(e => e.CommunicationId == id);
        }
    }
}