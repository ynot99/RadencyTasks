using AutoMapper;
using FluentValidation;
using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer.Services
{
    internal class BookBLL : IBookBLL
    {
        private readonly IBookDAL _bookDAL;
        private readonly IMapper _mapper;
        private readonly IValidator<BookDTO> _validator;

        public BookBLL(
            IBookDAL bookDAL, IMapper bookMapper, IValidator<BookDTO> validator)
        {
            _bookDAL = bookDAL;
            _mapper = bookMapper;
            _validator = validator;
        }

        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>> GetAllBooksInOrder(string? order)
        {
            return await _bookDAL.GetAllInOrder(order);
        }

        public async Task<ActionResult<List<BookRatingAvgReviewCntDTO>>>
            GetTop10HighestAnd10MoreReviewsRatedBooksByGenre(string? genre)
        {
            return await _bookDAL.GetLimitByGenreAndMoreThanReviews(10, 10, genre);
        }

        public async Task<BookWithRatingAndReviewListDTO>
            GetBooksWithRatingAndReviewList(long bookId)
        {

            return await _bookDAL.GetByIdDetailedWithRatingAndReviews(bookId);
        }

        public async Task<ActionResult<BookDTO>> SaveBook(BookDTO book)
        {
            try
            {
                _validator.ValidateAndThrow(book);
            }
            catch (ValidationException ex)
            {
                throw new ValidationFailedException(ex.Message);
            }
            BookDTO? bookDTO;
            try
            {
                bookDTO = _mapper.Map<Book, BookDTO>(await _bookDAL.SaveOrModify(book));
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            return bookDTO;
        }
    }
}