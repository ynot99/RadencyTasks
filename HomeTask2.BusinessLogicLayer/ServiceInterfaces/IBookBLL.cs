using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IBookBLL
    {
        public Task<ResponseDTO<BookDTO>> GetBookById(long bookId);
        public Task<ResponseDTO<List<BookRatingAvgReviewCntDTO>>> GetAllBooksInOrder(string? order);
        public Task<ResponseDTO<List<BookRatingAvgReviewCntDTO>>> GetTopRatedBooks(
            int bookCount, long reviewCount, string? genre);
        public Task<ResponseDTO<BookRatingAvgReviewListDTO>> GetBooksScoreAvgReviewList(
            long bookId);
        public Task<ResponseDTO<BookDTO>> SaveBook(BookDTO book);
        public Task<ResponseDTO<IdResponseDTO>> DeleteBook(long bookId);
    }
}
