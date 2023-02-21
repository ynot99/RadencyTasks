using AutoMapper;
using HomeTask2.BusinessLogicLayer.AutoMapperProfiles;
using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer
{
    public class BookBLL
    {
        private readonly DataAccessLayer.BookDAL? _bookDAL;
        private readonly Mapper? _bookMapper;

        public BookBLL(DataAccessLayer.BookDAL bookDAL, Mapper bookMapper)
        {
            _bookDAL = bookDAL;
            _bookMapper = bookMapper;
        }

        // TODO it should be async
        public async Task<ActionResult<BookRatingReviewProfile>> GetAllBooks()
        {
            IAsyncEnumerable<Book> booksFromDB = _bookDAL.GetAllBooks();
            BookRatingReviewProfile booksModel
                = _bookMapper.Map<BookRatingReviewProfile>(booksFromDB);
            return booksModel;
        }
    }
}