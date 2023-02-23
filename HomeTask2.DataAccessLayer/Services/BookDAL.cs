using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer.Services
{
    internal class BookDAL : IBookDAL
    {
        private readonly HomeTask2Context _context;

        public BookDAL(HomeTask2Context context)
        {
            _context = context;
        }

        private static IQueryable<BookRatingAvgReviewCntDTO>
            ExtendQuery(IQueryable<Book> query)
        {
            // TODO Maybe we want to return entities combined instead of DTO
            return query
                .Select(book => new BookRatingAvgReviewCntDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ReviewsNumber = book.Reviews != null ? book.Reviews.Count : 0,
                    // DANGER I was forced to use null! forgiving operators, otherwise
                    // the app crashes on .OrderByDescending(book => book.Rating)
                    Rating = book.Ratings!.Any() ? book.Ratings!.Average(book => book.Score) : 0
                });
        }

        public async Task<List<BookRatingAvgReviewCntDTO>> GetAllInOrder(string? order)
        {
            IQueryable<BookRatingAvgReviewCntDTO> query
                = ExtendQuery(_context.Books);

            return order switch
            {
                "author" => await query.OrderBy(book => book.Author).ToListAsync(),
                "title" => await query.OrderBy(book => book.Title).ToListAsync(),
                _ => await query.ToListAsync(),
            };
        }

        public async Task<List<BookRatingAvgReviewCntDTO>>
            GetLimitByGenreAndMoreThanReviews(int limit, long reviewsCount, string? genre)
        {
            return await ExtendQuery(_context.Books
                .Where(book =>
                    (genre == null || book.Genre == genre)
                    && book.Reviews != null && book.Reviews.Count > reviewsCount))
                .OrderByDescending(book => book.Rating)
                .Take(limit).ToListAsync();
        }

        // BAD I shouldn't do manual mapping here,
        // database processes need to be divided by their service files
        public Task<BookWithRatingAndReviewListDTO>
            GetByIdDetailedWithRatingAndReviews(long id)
        {
            Book? existingBook = _context.Books.FirstOrDefault(book => book.Id == id);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }
            BookWithRatingAndReviewListDTO detailedBook = new BookWithRatingAndReviewListDTO()
            {
                Id = existingBook.Id,
                Author = existingBook.Author,
                Content = existingBook.Content,
                Cover = existingBook.Cover,
                Title = existingBook.Title,
                Rating = existingBook.Ratings != null
                    ? existingBook.Ratings.Average(review => review.Score) : 0,
                Reviews = existingBook.Reviews != null
                    ? existingBook.Reviews.Select(review =>
                    new ReviewListForBookDTO
                    {
                        Id = review.Id,
                        Message = review.Message,
                        Reviewer = review.Reviewer,
                    }
                    ).ToList()
                    : new List<ReviewListForBookDTO>(),
            };
            return detailedBook;
        }

        public async Task<Book> SaveOrModify(BookDTO book)
        {
            if (book.Id == 0)
            {
                Book newBook = new()
                {
                    Id = 0,
                    Author = book.Author,
                    Content = book.Content,
                    Cover = book.Cover,
                    Genre = book.Genre,
                    Title = book.Title
                };
                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                return newBook;
            }
            else
            {
                Book? existingBook = await _context.Books.FindAsync(book.Id);
                if (existingBook == null)
                    throw new EntityNotFoundException();
                existingBook.Title = book.Title;
                existingBook.Cover = book.Cover;
                existingBook.Content = book.Content;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                await _context.SaveChangesAsync();
                return existingBook;
            }
        }
    }
}
