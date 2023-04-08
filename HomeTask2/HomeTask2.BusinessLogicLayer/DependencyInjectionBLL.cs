using FluentValidation;
using HomeTask2.BusinessLogicLayer.AutoMapperProfiles;
using HomeTask2.BusinessLogicLayer.ServiceInterfaces;
using HomeTask2.BusinessLogicLayer.Services;
using HomeTask2.BusinessLogicLayer.Validators;
using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask2.BusinessLogicLayer
{
    public static class DependencyInjectionBLL
    {
        public static IServiceCollection ConfigureServicesBLL(this
            IServiceCollection services)
        {
            services.AddScoped<IValidator<BookDTO>, BookValidator>();
            services.AddScoped<IValidator<RatingScoreDTO>, RatingScoreValidator>();
            services.AddScoped<IValidator<ReviewContentDTO>, ReviewContentValidator>();

            services.AddScoped<IBookBLL, BookBLL>();
            services.AddScoped<IRatingBLL, RatingBLL>();
            services.AddScoped<IReviewBLL, ReviewBLL>();

            services.AddAutoMapper(
                typeof(BookMappingProfile),
                typeof(RatingMappingProfile),
                typeof(ReviewMappingProfile));

            services.ConfigureServicesDAL();

            return services;
        }
    }
}
