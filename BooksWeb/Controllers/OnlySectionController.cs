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
    [Route("api/OnlySection")]
    public class OnlySectionController : Controller
    {
        private readonly BookingClubContext _context;

        public OnlySectionController(BookingClubContext context)
        {
            _context = context;
        }

        // GET: api/OnlySection
        [HttpGet]
        public IEnumerable<Sections> GetSections()
        {
            return _context.Sections;
        }

        // GET: api/OnlySection/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSections([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sections = await _context.Sections.SingleOrDefaultAsync(m => m.SectionId == id);

            if (sections == null)
            {
                return NotFound();
            }

            return Ok(sections);
        }

        // PUT: api/OnlySection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSections([FromRoute] int id, [FromBody] Sections sections)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sections.SectionId)
            {
                return BadRequest();
            }

            _context.Entry(sections).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionsExists(id))
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

        // POST: api/OnlySection
        [HttpPost]
        public async Task<IActionResult> PostSections([FromBody] Sections sections)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sections.Add(sections);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSections", new { id = sections.SectionId }, sections);
        }

        // DELETE: api/OnlySection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSections([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sections = await _context.Sections.SingleOrDefaultAsync(m => m.SectionId == id);
            if (sections == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(sections);
            await _context.SaveChangesAsync();

            return Ok(sections);
        }

        private bool SectionsExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}