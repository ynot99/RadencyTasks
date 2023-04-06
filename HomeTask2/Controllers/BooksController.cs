using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Shared;
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

        public BooksController(
            IBookBLL bookBLL,
            IRatingBLL ratingBLL,
            IReviewBLL reviewBLL,
            IConfiguration configuration)
        {
            _bookBLL = bookBLL;
            _ratingBLL = ratingBLL;
            _reviewBLL = reviewBLL;
            _configuration = configuration;
        }

        [HttpGet("onlybook/{id}")]
        public async Task<ActionResult<ApiResponse<BookDTO>>>
            GetBooksInOrder(long id)
        {
            return GenerateResponse(await _bookBLL.GetBookById(id));
        }

        // ### 1. Get all books. Order by provided value (title or author).
        // GET: api/Books?order=author|title
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<BookRatingAvgReviewCntDTO>>>>
            GetBooksInOrder([FromQuery] string? order)
        {
            return GenerateResponse(await _bookBLL.GetAllBooksInOrder(order));
        }

        // ### 2. Get top 10 books with high rating and number of reviews greater than 10.
        // You can filter books by specifying genre. Order by rating.
        // GET: api/Books/recommended?genre=<string>
        [HttpGet("recommended")]
        public async Task<ActionResult<ApiResponse<List<BookRatingAvgReviewCntDTO>>>>
            GetTop10HighestRatedAnd10MoreReviewsBooksByGenre([FromQuery] string? genre)
        {
            return GenerateResponse(await _bookBLL.GetTopRatedBooks(10, 10, genre));
        }

        // ### 3. Get book details with the list of reviews.
        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookRatingAvgReviewListDTO>>>
            GetBooksWithRatingAndReviewList(long id)
        {
            return GenerateResponse(await _bookBLL.GetBooksScoreAvgReviewList(id));
        }

        // ### 4. Delete a book using a secret key. Save the secret key in the config of your application.
        // Compare this key with a query param
        // DELETE: api/Books/{id}?secret=<string>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<IdResponseDTO>>> DeleteBookSecret(
            long id, [FromQuery] string? secret)
        {
            if (secret == null || secret != _configuration.GetValue<string>("secret"))
            {
                return BadRequest(new IdResponseDTO { Id = id });
            }
            return GenerateResponse(await _bookBLL.DeleteBook(id));
        }

        // ### 5. Save a new book.
        // POST: api/Books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public async Task<ActionResult<ApiResponse<BookDTO>>> PostBook(BookDTO book)
        {
            return GenerateResponse(await _bookBLL.SaveBook(book));
        }

        // ### 6. Save a review for the book.
        // PUT: api/Books/5/review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/review")]
        public async Task<ActionResult<ApiResponse<IdResponseDTO>>> SaveReview(long id, ReviewContentDTO review)
        {
            return GenerateResponse(await _reviewBLL.ReviewBook(id, review));
        }

        // ### 7. Rate a book.
        // PUT: api/Books/5/rate
        [HttpPut("{id}/rate")]
        public async Task<ActionResult<ApiResponse<RatingDTO>>> RateBook(long id, RatingScoreDTO DTOratingScore)
        {
            return GenerateResponse(await _ratingBLL.RateBook(id, DTOratingScore));
        }

        [HttpGet("throw")]
        public IActionResult Throw() =>
            throw new Exception("Sample exception.");

        private ActionResult<ApiResponse<T>> GenerateResponse<T>(
            ResponseDTO<T> DTOresponse)
        {
            ApiResponse<T> response = new(
                DTOresponse.Data, DTOresponse.Message);

            return DTOresponse.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(nameof(response), response),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                _ => throw new Exception(),
            };
        }
    }
}
