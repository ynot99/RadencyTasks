using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer
{
    internal class BookDAL : IBookDAL
    {
        private readonly HomeTask2Context _context;

        public BookDAL(HomeTask2Context context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll(string? order)
        {
            DbSet<Book> query = _context.Books;
            return order switch
            {
                "author" => await query.OrderBy(book => book.Author).ToListAsync(),
                "title" => await query.OrderBy(book => book.Title).ToListAsync(),
                _ => await query.ToListAsync(),
            };
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
