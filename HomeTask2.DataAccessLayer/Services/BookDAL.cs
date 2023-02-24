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

        public async Task<Book> GetById(long id)
        {
            Book? existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }
            return existingBook;
        }

        public async Task<Book> Update(BookDTO DTObook)
        {
            Book? existingBook = await _context.Books.FindAsync(DTObook.Id);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }
            existingBook.Title = DTObook.Title;
            existingBook.Cover = DTObook.Cover;
            existingBook.Content = DTObook.Content;
            existingBook.Author = DTObook.Author;
            existingBook.Genre = DTObook.Genre;
            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<Book> Create(BookDTO DTObook)
        {
            Book newBook = new()
            {
                Id = 0,
                Author = DTObook.Author,
                Content = DTObook.Content,
                Cover = DTObook.Cover,
                Genre = DTObook.Genre,
                Title = DTObook.Title
            };
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task Delete(long id)
        {
            Book? existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }

            _context.Books.Remove(existingBook);
            await _context.SaveChangesAsync();
        }

        private static IQueryable<BookRatingAvgReviewCntDTO> ExtendQuery(IQueryable<Book> query)
        {
            return query
                .Select(book => new BookRatingAvgReviewCntDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ReviewsNumber = book.Reviews != null ? book.Reviews.Count : 0,
                    // DANGER null!
                    Rating = book.Ratings!.Any() ? book.Ratings!.Average(rating => rating.Score) : 0
                    // Idk why, but this throws
                    // System.InvalidOperationException: Sequence contains no elements
                    //Rating = book.Ratings != null && book.Ratings.Any() 
                    //    ? book.Ratings.Average(book => book.Score) : 0
                });
        }

        public async Task<List<BookRatingAvgReviewCntDTO>> TakeBooksByCntRatingAvgByReviewCntByGenre(
            int bookCount, long reviewCount, string? genre)
        {
            return await ExtendQuery(_context.Books
                .Where(book =>
                        (genre == null || book.Genre == genre)
                        // DANGER null! to hide warn in console logs
                        && book.Reviews!.Any() == true && book.Reviews!.Count > reviewCount
                        )
                )
                .OrderByDescending(book => book.Rating)
                .Take(bookCount)
                .ToListAsync();
        }

        public async Task<List<BookRatingAvgReviewCntDTO>> GetAllRatingAvgReviewCntInOrder(string? order)
        {
            IQueryable<BookRatingAvgReviewCntDTO> query = ExtendQuery(_context.Books);

            return order switch
            {
                "title" => await query.OrderBy(book => book.Title).ToListAsync(),
                "author" => await query.OrderBy(book => book.Author).ToListAsync(),
                _ => await query.ToListAsync()
            };
        }

        public async Task<BookRatingAvgReviewListDTO> GetByIdDetailedWithRatingAndReviews(
            long id)
        {
            Book? existingBook = await _context.Books
                .Include(book => book.Ratings)
                .Include(book => book.Reviews)
                .FirstOrDefaultAsync(book => book.Id == id);

            if (existingBook == null)
            {
                throw new EntityNotFoundException();
            }
            BookRatingAvgReviewListDTO detailedBook = new()
            {
                Id = existingBook.Id,
                Author = existingBook.Author,
                Content = existingBook.Content,
                Cover = existingBook.Cover,
                Title = existingBook.Title,
                // DANGER null!
                Rating = existingBook.Ratings!.Any() ?
                    existingBook.Ratings!.Average(review => review.Score) : 0,
                Reviews = existingBook.Reviews != null
                    ? existingBook.Reviews.Select(review =>
                    new ReviewForBookDTO
                    {
                        Id = review.Id,
                        Message = review.Message,
                        Reviewer = review.Reviewer,
                    }
                    ).ToList()
                    : new List<ReviewForBookDTO>(),
            };
            return detailedBook;
        }
    }
}
