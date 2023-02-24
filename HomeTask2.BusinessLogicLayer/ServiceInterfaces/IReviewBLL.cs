using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.ServiceInterfaces
{
    public interface IReviewBLL
    {
        public Task<ResponseDTO<IdResponseDTO>> ReviewBook(
            long bookId, ReviewContentDTO reviewContentDTO);
    }
}
