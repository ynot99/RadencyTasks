using Microsoft.Extensions.DependencyInjection;

namespace HomeTask2.BusinessLogicLayer
{
    internal class DependencyInjection
    {
        internal static IServiceCollection ImplementPersistence(IServiceCollection services)
        {
            services.AddScoped<BookBLL>();
            services.AddAutoMapper(typeof(BookBLL));

            return services;
        }
    }
}
