using AutoMapper;
using FluentValidation;
using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer
{
    internal class BookBLL : IBookBLL
    {
        private readonly DataAccessLayer.IBookDAL _bookDAL;
        private readonly IMapper _bookMapper;
        private readonly IValidator<BookDTO> _validator;

        public BookBLL(
            DataAccessLayer.IBookDAL bookDAL, IMapper bookMapper, IValidator<BookDTO> validator)
        {
            _bookDAL = bookDAL;
            _bookMapper = bookMapper;
            _validator = validator;
        }

        // TODO it should be async
        public async Task<ActionResult<List<BookDTO>>> GetAllBooks(string? order)
        {
            List<BookDTO> booksDTO
                = _bookMapper.Map<List<Book>, List<BookDTO>>(await _bookDAL.GetAll(order));
            return booksDTO;
        }

        public async Task<ActionResult<BookDTO>> SaveBook(BookDTO book)
        {
            _validator.ValidateAndThrow(book);
            return _bookMapper.Map<Book, BookDTO>(await _bookDAL.SaveOrModify(book));
        }
    }
}