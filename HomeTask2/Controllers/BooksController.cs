using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly HomeTask2.BusinessLogicLayer.BookBLL _bookBLL;

        public BooksController(HomeTask2.BusinessLogicLayer.BookBLL bookBLL)
        {
            _bookBLL = bookBLL;
        }

        // TODO ### 1
        // GET: api/Books?order=author
        [HttpGet]
        public async Task<ActionResult<object>> GetBookItemsInOrder([FromQuery] string? order)
        {
            return await _bookBLL.GetAllBooks();
            //IQueryable<BookRatingReviewDTO> query = _mapper.ProjectTo<BookRatingReviewDTO>(_context.BookItems);
            //return order switch
            //{
            //    "author" => await query.OrderBy(book => book.Author).ToListAsync(),
            //    "title" => await query.OrderBy(book => book.Title).ToListAsync(),
            //    _ => await query.ToListAsync(),
            //};
        }

        // TODO ### 5
        // POST: api/Books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost("save")]
        //    public async Task<ActionResult<Book>> PostBook(Book book)
        //    {
        //        if (book.Id == 0)
        //        {
        //            _context.BookItems.Add(book);
        //            await _context.SaveChangesAsync();
        //            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        //        }
        //        else
        //        {
        //            Book? existingBook = await _context.BookItems.FindAsync(book.Id);
        //            if (existingBook == null)
        //                return NotFound();
        //            existingBook.Title = book.Title;
        //            existingBook.Cover = book.Cover;
        //            existingBook.Content = book.Content;
        //            existingBook.Author = book.Author;
        //            existingBook.Genre = book.Genre;
        //            await _context.SaveChangesAsync();
        //            return book;
        //        }
        //    }

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
