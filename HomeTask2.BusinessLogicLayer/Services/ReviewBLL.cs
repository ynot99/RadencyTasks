using AutoMapper;
using FluentValidation;
using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using System.Net;

namespace HomeTask2.BusinessLogicLayer.Services
{
    internal class ReviewBLL : IReviewBLL
    {
        private readonly IReviewDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IValidator<ReviewContentDTO> _validator;

        public ReviewBLL(IReviewDAL reviewDAL, IMapper reviewMapper, IValidator<ReviewContentDTO> validator)
        {
            _DAL = reviewDAL;
            _mapper = reviewMapper;
            _validator = validator;
        }

        public async Task<ResponseDTO<IdResponseDTO>> ReviewBook(
            long bookId, ReviewContentDTO DTOreviewContent)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(DTOreviewContent);
            }
            catch (ValidationException ex)
            {
                return new ResponseDTO<IdResponseDTO>(
                    HttpStatusCode.BadRequest,
                    ex.Message, new IdResponseDTO { Id = 0 });
            }
            Review? review;
            try
            {
                review = await _DAL.CreateByBookId(bookId, DTOreviewContent);
            }
            catch (EntityNotFoundException ex)
            {
                return new ResponseDTO<IdResponseDTO>(
                    HttpStatusCode.NotFound,
                    ex.Message, new IdResponseDTO { Id = 0 });
            }
            return new ResponseDTO<IdResponseDTO>(
                HttpStatusCode.Created,
                $"Successfully created a new review for a book with id {bookId}.",
                _mapper.Map<Review, IdResponseDTO>(review));
        }
    }
}
