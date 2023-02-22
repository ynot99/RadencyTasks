using HomeTask2.DataAccessLayer.Repository;
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

            services.AddScoped<IBookDAL, BookDAL>();
            services.AddScoped<IHomeTask2Context, HomeTask2Context>();

            return services;
        }
    }
}
