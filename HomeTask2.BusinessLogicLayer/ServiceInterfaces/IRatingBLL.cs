using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IRatingBLL
    {
        public Task<ActionResult<RatingDTO>> RateBook(long bookId, RatingScoreDTO ratingScoreDTO);
    }
}
