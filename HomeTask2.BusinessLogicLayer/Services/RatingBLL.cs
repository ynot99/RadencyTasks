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

        public async Task<ResponseDTO<RatingDTO>> RateBook(long bookId, RatingScoreDTO DTOratingScore)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(DTOratingScore);
            }
            catch (ValidationException ex)
            {
                return new ResponseDTO<RatingDTO>(
                    HttpStatusCode.BadRequest,
                    ex.Message, _mapper.Map<RatingScoreDTO, RatingDTO>(DTOratingScore));
            }
            Rating? rating;
            try
            {
                rating = await _DAL.CreateByBookId(bookId, DTOratingScore);
            }
            catch (EntityNotFoundException ex)
            {
                return new ResponseDTO<RatingDTO>(
                    HttpStatusCode.NotFound,
                    ex.Message, _mapper.Map<RatingScoreDTO, RatingDTO>(DTOratingScore));
            }
            return new ResponseDTO<RatingDTO>(
                HttpStatusCode.Created,
                $"Successfully created a new rating for a book with id {bookId}.",
                _mapper.Map<Rating, RatingDTO>(rating));
        }
    }
}
