using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;

namespace HomeTask2.DataAccessLayer.Services
{
    internal class ReviewDAL : IReviewDAL
    {
        private readonly HomeTask2Context _context;

        public ReviewDAL(HomeTask2Context context)
        {
            _context = context;
        }

        public async Task<Review> CreateByBookId(long bookId, ReviewContentDTO reviewContentDTO)
        {
            Book? existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }

            Review newReview = new()
            {
                Id = 0,
                Message = reviewContentDTO.Message,
                Book = existingBook,
                Reviewer = reviewContentDTO.Reviewer,
            };
            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();
            return newReview;
        }
    }
}
