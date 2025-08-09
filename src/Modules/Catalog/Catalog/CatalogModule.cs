﻿using Catalog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder;
        }
    }
}
