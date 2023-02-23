using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer.ServiceInterfaces
{
    public interface IReviewDAL
    {
        public Task<Review> ReviewBook(long bookId, ReviewContentDTO reviewContentDTO);
    }
}
