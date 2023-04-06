using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer.ServiceInterfaces
{
    public interface IBookDAL
    {
        public Task<List<BookRatingAvgReviewCntDTO>> GetAllRatingAvgReviewCntInOrder(string? order);
        public Task<List<BookRatingAvgReviewCntDTO>> TakeBooksByCntRatingAvgByReviewCntByGenre(
            int bookCount, long reviewCount, string? genre);
        public Task<BookRatingAvgReviewListDTO> GetByIdDetailedWithRatingAndReviews(long id);
        public Task<Book> Update(BookDTO DTObook);
        public Task<Book> Create(BookDTO DTObook);
        public Task Delete(long id);
    }
}
