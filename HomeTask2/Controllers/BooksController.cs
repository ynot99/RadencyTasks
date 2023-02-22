using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly HomeTask2.BusinessLogicLayer.IBookBLL _bookBLL;

        public BooksController(HomeTask2.BusinessLogicLayer.IBookBLL bookBLL)
        {
            _bookBLL = bookBLL;
        }

        // TODO ### 1
        // GET: api/Books?order=author
        // TODO it must return Task<TResult>
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBookItemsInOrder([FromQuery] string? order)
        {
            return await _bookBLL.GetAllBooks(order);
        }

        //TODO ### 5
        // POST: api/Books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
        {
            // TODO return NotFound if book doesn't exist in DB using overver pattern
            return await _bookBLL.SaveBook(book);
        }

        //    // TODO ### 7
        //    // GET: api/Books/5/rate
        //    [HttpGet("{id}/rate")]
        //    public async Task<ActionResult<Book>> RateBook(long id)
        //    {
        //        var book = await _context.BookItems.FindAsync(id);

        //        if (book == null)
        //        {
        //            return NotFound();
        //        }

        //        return book;
        //    }

        //    // GET: api/Books/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Book>> GetBook(long id)
        //    {
        //        var book = await _context.BookItems.FindAsync(id);

        //        if (book == null)
        //        {
        //            return NotFound();
        //        }

        //        return book;
        //    }

        //    // PUT: api/Books/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutBook(long id, Book book)
        //    {
        //        if (id != book.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(book).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // DELETE: api/Books/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteBook(long id)
        //    {
        //        var book = await _context.BookItems.FindAsync(id);
        //        if (book == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.BookItems.Remove(book);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool BookExists(long id)
        //    {
        //        return _context.BookItems.Any(e => e.Id == id);
        //    }
    }
}
