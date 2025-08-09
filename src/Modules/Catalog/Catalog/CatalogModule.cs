
using Catalog.Data.Seed;
using Shared.Data.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));
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
