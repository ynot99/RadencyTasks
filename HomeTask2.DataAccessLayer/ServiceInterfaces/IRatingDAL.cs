using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer.ServiceInterfaces
{
    public interface IRatingDAL
    {
        public Task<List<Rating>> GetAll();
        public Task<decimal> GetAverageScoreByBookId(long bookId);

        public Task<Rating> CreateByBookId(long bookId, RatingScoreDTO score);
    }
}
