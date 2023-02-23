using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookBLL _bookBLL;
        private readonly IRatingBLL _ratingBLL;
        private readonly IReviewBLL _reviewBLL;

        public BooksController(IBookBLL bookBLL, IRatingBLL ratingBLL, IReviewBLL reviewBLL)
        {
            _bookBLL = bookBLL;
            _ratingBLL = ratingBLL;
            _reviewBLL = reviewBLL;
        }

        // ### 1
        // GET: api/Books?order=author|title
        [HttpGet]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetBooksInOrder([FromQuery] string? order)
        {
            return await _bookBLL.GetAllBooksInOrder(order);
        }

        // ### 2
        // GET: api/Books/recommended?genre=<string>
        [HttpGet("recommended")]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetTop10HighestRatedAnd10MoreReviewsBooksByGenre([FromQuery] string? genre)
        {
            return await _bookBLL.GetTop10HighestAnd10MoreReviewsRatedBooksByGenre(genre);
        }

        // ### 3
        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetBooksWithRatingAndReviewList(long id)
        {
            throw new NotImplementedException();
        }

        // ### 4
        // DELETE: api/Books/{id}?secret=<string>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            DeleteBookSecret(long id, [FromQuery] string? secret)
        {
            throw new NotImplementedException();
        }

        // ### 5
        // TODO receive image and save in base64 format
        // POST: api/Books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
        {
            // TODO return NotFound if book doesn't exist in DB
            // TODO add IsNew to Book model and check if it is new,
            // so we can return appropriate status code
            return await _bookBLL.SaveBook(book);
        }

        // TODO ### 6
        // PUT: api/Books/5/review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/review")]
        public async Task<ActionResult<IdResponseDTO>> SaveReview(long id, ReviewContentDTO review)
        {
            ActionResult<IdResponseDTO> reviewDTO;
            try
            {
                reviewDTO = await _reviewBLL.ReviewBook(id, review);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return reviewDTO;
        }

        // TODO ### 7
        // PUT: api/Books/5/rate
        [HttpPut("{id}/rate")]
        public async Task<ActionResult<RatingDTO>> RateBook(long id, RatingScoreDTO ratingScoreDTO)
        {
            ActionResult<RatingDTO> ratingDTO;
            try
            {
                ratingDTO = await _ratingBLL.RateBook(id, ratingScoreDTO);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return ratingDTO;
        }

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
