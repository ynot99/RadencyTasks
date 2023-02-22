using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer
{
    public interface IBookBLL
    {
        public Task<ActionResult<List<BookDTO>>> GetAllBooks(string? order);
        public Task<ActionResult<BookDTO>> SaveBook(BookDTO book);
    }
}
