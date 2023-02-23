using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IBookBLL
    {
        public Task<ActionResult<List<BookRatingAvgReviewCntDTO>>> GetAllBooksInOrder(string? order);
        public Task<ActionResult<List<BookRatingAvgReviewCntDTO>>> GetTop10HighestAnd10MoreReviewsRatedBooksByGenre(string? genre);
        public Task<ActionResult<BookDTO>> SaveBook(BookDTO book);
    }
}
