using AutoMapper;
using FluentValidation;
using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.Core.DTOs;
using HomeTask2.Core.Exceptions;
using HomeTask2.DataAccessLayer.Repository.Entities;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult<IdResponseDTO>> ReviewBook(
            long bookId, ReviewContentDTO reviewContentDTO)
        {
            try
            {
                _validator.ValidateAndThrow(reviewContentDTO);
            }
            catch (ValidationException ex)
            {
                throw new ValidationFailedException(ex.Message);
            }
            Review? review;
            try
            {
                review = await _DAL.ReviewBook(bookId, reviewContentDTO);
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            return _mapper.Map<Review, IdResponseDTO>(review);
        }
    }
}
