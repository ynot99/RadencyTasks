// TODO do we need this?
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace HomeTask2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RatingsController : ControllerBase
//    {
//        private readonly HomeTask2Context _context;

//        public RatingsController(HomeTask2Context context)
//        {
//            _context = context;
//        }

//        // GET: api/Ratings
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Rating>>> GetRatingItems()
//        {
//            return await _context.RatingItems.ToListAsync();
//        }

//        // GET: api/Ratings/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Rating>> GetRating(long id)
//        {
//            var rating = await _context.RatingItems.FindAsync(id);

//            if (rating == null)
//            {
//                return NotFound();
//            }

//            return rating;
//        }

//        // PUT: api/Ratings/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRating(long id, Rating rating)
//        {
//            if (id != rating.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(rating).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!RatingExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Ratings
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Rating>> PostRating(Rating rating)
//        {
//            _context.RatingItems.Add(rating);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
//        }

//        // DELETE: api/Ratings/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRating(long id)
//        {
//            var rating = await _context.RatingItems.FindAsync(id);
//            if (rating == null)
//            {
//                return NotFound();
//            }

//            _context.RatingItems.Remove(rating);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool RatingExists(long id)
//        {
//            return _context.RatingItems.Any(e => e.Id == id);
//        }
//    }
//}
