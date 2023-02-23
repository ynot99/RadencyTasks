using HomeTask2.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IReviewBLL
    {
        public Task<ActionResult<IdResponseDTO>> ReviewBook(
            long bookId, ReviewContentDTO reviewContentDTO);
    }
}
