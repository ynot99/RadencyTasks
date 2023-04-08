using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IRatingBLL
    {
        public Task<ResponseDTO<RatingDTO>> RateBook(long bookId, RatingScoreDTO ratingScoreDTO);
    }
}
