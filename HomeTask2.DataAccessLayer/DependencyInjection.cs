using HomeTask2.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask2.DataAccessLayer
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection ImplementPersistence(IServiceCollection services)
        {
            services.AddDbContext<HomeTask2Context>(
                options => options.UseInMemoryDatabase("HomeTask2"));

            services.AddScoped<IHomeTask2Context, HomeTask2Context>();

            return services;
        }
    }
}
