using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder applicationBuilder)
            where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(applicationBuilder).GetAwaiter().GetResult();

            SeedDataAsync(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();

            return applicationBuilder;
        }

        private static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach (var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }

        private static async Task MigrateDatabaseAsync<TContext>(IApplicationBuilder applicationBuilder)
                        where TContext : DbContext
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            await context.Database.MigrateAsync();
        }
    }
}
