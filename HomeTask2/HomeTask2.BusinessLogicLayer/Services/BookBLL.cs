using AutoMapper;
using FluentValidation;
using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using System.Net;

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

        public async Task<ResponseDTO<BookDTO>> GetBookById(long bookId)
        {
            BookDTO existingBook;
            try
            {
                existingBook = _mapper.Map<Book, BookDTO>(await _bookDAL.GetById(bookId));
            }
            catch (EntityNotFoundException ex)
            {
                return new ResponseDTO<BookDTO>(
                    HttpStatusCode.NotFound, ex.Message);
            }

            return new ResponseDTO<BookDTO>(
                HttpStatusCode.OK,
                "Successfully retrieved a book with rating and reviews.",
                existingBook);
        }

        public async Task<ResponseDTO<List<BookRatingAvgReviewCntDTO>>> GetAllBooksInOrder(string? order)
        {
            // BAD should DAL return DTO?
            List<BookRatingAvgReviewCntDTO> DBbooks =
                await _bookDAL.GetAllRatingAvgReviewCntInOrder(order);

            return new ResponseDTO<List<BookRatingAvgReviewCntDTO>>(
                HttpStatusCode.OK,
                "Successfully retrieved books with rating and reviews count.",
                DBbooks);
        }

        public async Task<ResponseDTO<List<BookRatingAvgReviewCntDTO>>> GetTopRatedBooks(
            int bookCount, long reviewCount, string? genre)
        {
            List<BookRatingAvgReviewCntDTO> DBbooks =
                await _bookDAL.TakeBooksByCntRatingAvgByReviewCntByGenre(
                    bookCount, reviewCount, genre);

            return new ResponseDTO<List<BookRatingAvgReviewCntDTO>>(
                HttpStatusCode.OK,
                $"Successfully retrieved the highest rated books by genre {genre} with rating and reviews.",
                DBbooks);
        }

        public async Task<ResponseDTO<BookRatingAvgReviewListDTO>> GetBooksScoreAvgReviewList(
            long bookId)
        {
            BookRatingAvgReviewListDTO existingBook;
            try
            {
                existingBook = await _bookDAL.GetByIdDetailedWithRatingAndReviews(bookId);
            }
            catch (EntityNotFoundException ex)
            {
                return new ResponseDTO<BookRatingAvgReviewListDTO>(
                    HttpStatusCode.NotFound, ex.Message);
            }

            return new ResponseDTO<BookRatingAvgReviewListDTO>(
                HttpStatusCode.OK,
                "Successfully retrieved a book with rating and reviews.",
                existingBook);
        }

        public async Task<ResponseDTO<BookDTO>> SaveBook(BookDTO DTObook)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(DTObook);
            }
            catch (ValidationException ex)
            {
                return new ResponseDTO<BookDTO>(
                    HttpStatusCode.BadRequest, ex.Message, DTObook);
            }
            if (DTObook.Id == null)
            {
                Book newBook = await _bookDAL.Create(DTObook);
                return new ResponseDTO<BookDTO>(
                    HttpStatusCode.Created,
                    "Book was created successfully",
                    _mapper.Map<Book, BookDTO>(newBook));
            }
            else
            {
                Book updatedBook;
                try
                {
                    updatedBook = await _bookDAL.Update(DTObook);
                }
                catch (EntityNotFoundException ex)
                {
                    return new ResponseDTO<BookDTO>(
                        HttpStatusCode.NotFound,
                        ex.Message,
                        DTObook);
                }
                return new ResponseDTO<BookDTO>(
                    HttpStatusCode.OK,
                    "Book was updated successfully",
                    _mapper.Map<Book, BookDTO>(updatedBook));
            }
        }

        public async Task<ResponseDTO<IdResponseDTO>> DeleteBook(long bookId)
        {
            try
            {
                await _bookDAL.Delete(bookId);
            }
            catch (EntityNotFoundException ex)
            {
                return new ResponseDTO<IdResponseDTO>(
                    HttpStatusCode.NotFound,
                    ex.Message,
                    new IdResponseDTO { Id = bookId });
            }
            return new ResponseDTO<IdResponseDTO>(HttpStatusCode.NoContent,
                $"A book with id {bookId} was deleted successfully.");
        }
    }
}
