using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer.Services
{
    internal class RatingDAL : IRatingDAL
    {
        private readonly HomeTask2Context _context;

        public RatingDAL(HomeTask2Context context)
        {
            _context = context;
        }
        public async Task<List<Rating>> GetAll()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<decimal> GetAverageScoreByBookId(long bookId)
        {
            Book? existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }

            return existingBook.Ratings?.Any() == true
                ? existingBook.Ratings.Average(rating => rating.Score) : 0;
        }

        public async Task<Rating> CreateByBookId(long bookId, RatingScoreDTO ratingScoreDTO)
        {
            Book? existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }

            Rating newRating = new()
            {
                Id = 0,
                Book = existingBook,
                Score = ratingScoreDTO.Score
            };
            _context.Ratings.Add(newRating);
            await _context.SaveChangesAsync();
            return newRating;
        }
    }
}
