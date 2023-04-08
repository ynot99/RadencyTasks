using AutoMapper;
using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.BusinessLogicLayer.AutoMapperProfiles
{
    public class RatingMappingProfile : Profile
    {
        public RatingMappingProfile()
        {
            CreateMap<Rating, RatingDTO>();
            CreateMap<RatingScoreDTO, RatingDTO>();
        }
    }
}
