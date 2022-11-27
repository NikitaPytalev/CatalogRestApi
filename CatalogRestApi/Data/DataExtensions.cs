using CatalogRestApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogRestApi.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, bool isDevelopment)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            return services
                .AddDbContextPool<CatalogDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ranker");
                    options.EnableDetailedErrors(isDevelopment);
                    options.EnableSensitiveDataLogging(isDevelopment);
                })
                .AddScoped<ICatalogDbContext>(provider => provider.GetRequiredService<CatalogDbContext>());
        }
    }
}
