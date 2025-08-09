
using Catalog.Data.Seed;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Shared.Data.Interceptors;
using Shared.Data.Seed;
using System.Reflection;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<CatalogDbContext>((sp,options) =>
            {
                options.UseNpgsql(connectionString);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
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
