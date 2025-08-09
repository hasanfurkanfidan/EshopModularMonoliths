
using Catalog.Data.Seed;
using Microsoft.IdentityModel.Tokens;
using Shared.Data.Interceptors;
using Shared.Data.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.AddInterceptors(new AuditableEntityInterceptor());
            });
            services.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMigration<CatalogDbContext>();
            return applicationBuilder;
        }
    }
}
