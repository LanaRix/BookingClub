using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace BooksWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/OnlyBook")]
    public class OnlyBookController : Controller
    {
        private readonly BookingClubContext _context;

        public OnlyBookController(BookingClubContext context)
        {
            _context = context;
        }

        // GET: api/OnlyBook
        [HttpGet]
        public IEnumerable<Books> GetBooks()
        {
            return _context.Books;
        }

        // GET: api/OnlyBook/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/OnlyBook/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBooks([FromRoute] int id, [FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != books.BookId)
            {
                return BadRequest();
            }

            _context.Entry(books).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
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

        // POST: api/OnlyBook
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostBooks([FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(books);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooks", new { id = books.BookId }, books);
        }

        // DELETE: api/OnlyBook/5
        [HttpDelete("{id}")]
        //[Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBooks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            if (books == null)
            {
                return NotFound();
            }

            _context.Books.Remove(books);
            await _context.SaveChangesAsync();

            return Ok(books);
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}