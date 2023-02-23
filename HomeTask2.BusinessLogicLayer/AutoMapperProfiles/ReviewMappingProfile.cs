﻿using AutoMapper;
using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.BusinessLogicLayer.AutoMapperProfiles
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, IdResponseDTO>();
        }
    }
}
