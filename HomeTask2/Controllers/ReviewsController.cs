﻿// TODO do we need this?
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using HomeTask2.Models;

//namespace HomeTask2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ReviewsController : ControllerBase
//    {
//        private readonly HomeTask2Context _context;

//        public ReviewsController(HomeTask2Context context)
//        {
//            _context = context;
//        }

//        // GET: api/Reviews
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Review>>> GetReviewItems()
//        {
//            return await _context.ReviewItems.ToListAsync();
//        }

//        // GET: api/Reviews/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Review>> GetReview(long id)
//        {
//            var review = await _context.ReviewItems.FindAsync(id);

//            if (review == null)
//            {
//                return NotFound();
//            }

//            return review;
//        }

//        // PUT: api/Reviews/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutReview(long id, Review review)
//        {
//            if (id != review.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(review).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ReviewExists(id))
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

//        // POST: api/Reviews
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Review>> PostReview(Review review)
//        {
//            _context.ReviewItems.Add(review);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetReview", new { id = review.Id }, review);
//        }

//        // DELETE: api/Reviews/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteReview(long id)
//        {
//            var review = await _context.ReviewItems.FindAsync(id);
//            if (review == null)
//            {
//                return NotFound();
//            }

//            _context.ReviewItems.Remove(review);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ReviewExists(long id)
//        {
//            return _context.ReviewItems.Any(e => e.Id == id);
//        }
//    }
//}
