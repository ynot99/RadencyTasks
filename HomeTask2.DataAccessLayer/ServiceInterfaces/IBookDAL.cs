using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer.ServiceInterfaces
{
    public interface IBookDAL
    {
        public Task<List<BookRatingAvgReviewCntDTO>> GetAllInOrder(string? order);
        public Task<List<BookRatingAvgReviewCntDTO>> GetLimitByGenreAndMoreThanReviews(
            int limit, long reviewsCount, string? genre);
        public Task<BookWithRatingAndReviewListDTO> GetByIdDetailedWithRatingAndReviews(long id);
        public Task<Book> SaveOrModify(BookDTO book);
    }
}
