using AutoMapper;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.AutoMapperProfiles
{
    public class BookRatingReviewProfile : Profile
    {
        public BookRatingReviewProfile()
        {
            CreateMap<BookDTO, BookRatingReviewDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            //CreateMap<RatingModel, BookRatingReviewDTO>()
            //    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Score));

            //CreateMap<Review, BookRatingReviewDTO>()
            //    .ForMember(dest => dest.ReviewsNumber, opt => opt.MapFrom(src => src.Rev));
        }
    }
}
