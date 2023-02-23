using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.ServiceInterfaces;
using HomeTask2.DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask2.DataAccessLayer
{
    public static class DependencyInjectionDAL
    {
        public static IServiceCollection ConfigureServicesDAL(this
            IServiceCollection services)
        {
            services.AddDbContext<HomeTask2Context>(
                options => options.UseInMemoryDatabase("HomeTask2"));
            services.AddScoped<IHomeTask2Context, HomeTask2Context>();

            services.AddScoped<IBookDAL, BookDAL>();
            services.AddScoped<IRatingDAL, RatingDAL>();
            services.AddScoped<IReviewDAL, ReviewDAL>();

            return services;
        }
    }
}
