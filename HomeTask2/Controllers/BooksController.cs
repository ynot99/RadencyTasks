using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookBLL _bookBLL;
        private readonly IRatingBLL _ratingBLL;
        private readonly IReviewBLL _reviewBLL;
        private readonly IConfiguration _configuration;

        public BooksController(IBookBLL bookBLL, IRatingBLL ratingBLL, IReviewBLL reviewBLL,
            IConfiguration configuration)
        {
            _bookBLL = bookBLL;
            _ratingBLL = ratingBLL;
            _reviewBLL = reviewBLL;
            _configuration = configuration;
        }

        // ### 1. Get all books. Order by provided value (title or author).
        // GET: api/Books?order=author|title
        [HttpGet]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetBooksInOrder([FromQuery] string? order)
        {
            ResponseDTO<List<BookRatingAvgReviewCntDTO>> DTObookResponse
                = await _bookBLL.GetAllBooksInOrder(order);
            return DTObookResponse.StatusCode switch
            {
                HttpStatusCode.OK => Ok(DTObookResponse.Data),
                _ => throw new Exception(),
            };
        }

        // ### 2. Get top 10 books with high rating and number of reviews greater than 10.
        // You can filter books by specifying genre. Order by rating.
        // GET: api/Books/recommended?genre=<string>
        [HttpGet("recommended")]
        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetTop10HighestRatedAnd10MoreReviewsBooksByGenre([FromQuery] string? genre)
        {
            ResponseDTO<List<BookRatingAvgReviewCntDTO>> DTObookResponse
                = await _bookBLL.GetTopRatedBooks(10, 10, genre);
            return DTObookResponse.StatusCode switch
            {
                HttpStatusCode.OK => Ok(DTObookResponse.Data),
                _ => throw new Exception(),
            };
        }

        // ### 3. Get book details with the list of reviews.
        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRatingAvgReviewListDTO>>
            GetBooksWithRatingAndReviewList(long id)
        {
            ResponseDTO<BookRatingAvgReviewListDTO> DTObookResponse
                = await _bookBLL.GetBooksScoreAvgReviewList(id);
            return DTObookResponse.StatusCode switch
            {
                HttpStatusCode.OK => Ok(DTObookResponse.Data),
                HttpStatusCode.NotFound => NotFound(DTObookResponse.Data),
                _ => throw new Exception(),
            };
        }

        // ### 4. Delete a book using a secret key. Save the secret key in the config of your application.
        // Compare this key with a query param
        // DELETE: api/Books/{id}?secret=<string>
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdResponseDTO>> DeleteBookSecret(
            long id, [FromQuery] string? secret)
        {
            if (secret == null || secret != _configuration.GetValue<string>("secret"))
            {
                return BadRequest(new IdResponseDTO { Id = id });
            }
            ResponseDTO<IdResponseDTO> idOrEmptyResponse = await _bookBLL.DeleteBook(id);
            return idOrEmptyResponse.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(idOrEmptyResponse.Data),
                _ => throw new Exception(),
            };
        }

        // ### 5. Save a new book.
        // POST: api/Books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
        {
            ResponseDTO<BookDTO> DTObookResponse = await _bookBLL.SaveBook(book);
            return DTObookResponse.StatusCode switch
            {
                HttpStatusCode.OK => Ok(DTObookResponse.Data),
                HttpStatusCode.Created => Created(nameof(DTObookResponse.Data), DTObookResponse.Data),
                HttpStatusCode.BadRequest => BadRequest(DTObookResponse.Data),
                HttpStatusCode.NotFound => NotFound(DTObookResponse.Data),
                _ => throw new Exception(),
            };
        }

        // ### 6. Save a review for the book.
        // PUT: api/Books/5/review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/review")]
        public async Task<ActionResult<IdResponseDTO>> SaveReview(long id, ReviewContentDTO review)
        {
            ResponseDTO<IdResponseDTO> DTOreview = await _reviewBLL.ReviewBook(id, review);

            return DTOreview.StatusCode switch
            {
                HttpStatusCode.Created => Created(nameof(DTOreview.Data), DTOreview.Data),
                HttpStatusCode.BadRequest => BadRequest(DTOreview.Data),
                HttpStatusCode.NotFound => NotFound(DTOreview.Data),
                _ => throw new Exception(),
            };
        }

        // ### 7. Rate a book.
        // PUT: api/Books/5/rate
        [HttpPut("{id}/rate")]
        public async Task<ActionResult<RatingDTO>> RateBook(long id, RatingScoreDTO DTOratingScore)
        {
            ResponseDTO<RatingDTO> DTOrating = await _ratingBLL.RateBook(id, DTOratingScore);

            return DTOrating.StatusCode switch
            {
                HttpStatusCode.Created => Created(nameof(DTOrating.Data), DTOrating.Data),
                HttpStatusCode.BadRequest => BadRequest(DTOrating.Data),
                HttpStatusCode.NotFound => NotFound(DTOrating.Data),
                _ => throw new Exception(),
            };
        }

        [HttpGet("throw")]
        public IActionResult Throw() =>
            throw new Exception("Sample exception.");
    }
}
