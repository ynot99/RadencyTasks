using FluentValidation;
using HomeTask2.BusinessLogicLayer.AutoMapperProfiles;
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
            services.AddScoped<IBookBLL, BookBLL>();
            services.AddAutoMapper(typeof(BookMappingProfile), typeof(BookRatingReviewProfile));
            services.ConfigureServicesDAL();

            return services;
        }
    }
}
