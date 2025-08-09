using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder applicationBuilder)
            where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(applicationBuilder).GetAwaiter().GetResult();

            return applicationBuilder;
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
