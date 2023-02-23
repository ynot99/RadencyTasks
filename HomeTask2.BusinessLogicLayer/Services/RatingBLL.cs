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
    internal class RatingBLL : IRatingBLL
    {
        private readonly IRatingDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IValidator<RatingScoreDTO> _validator;

        public RatingBLL(IRatingDAL ratingDAL, IMapper ratingMapper, IValidator<RatingScoreDTO> validator)
        {
            _DAL = ratingDAL;
            _mapper = ratingMapper;
            _validator = validator;
        }

        public async Task<ActionResult<RatingDTO>> RateBook(long bookId, RatingScoreDTO ratingScoreDTO)
        {
            try
            {
                _validator.ValidateAndThrow(ratingScoreDTO);
            }
            catch (ValidationException ex)
            {
                throw new ValidationFailedException(ex.Message);
            }
            Rating? rating;
            try
            {
                rating = await _DAL.RateBook(bookId, ratingScoreDTO);
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            return _mapper.Map<Rating, RatingDTO>(rating);
        }
    }
}
