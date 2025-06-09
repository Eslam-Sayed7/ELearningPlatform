using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions.Migrations {

    public static class MigrateExtention {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
        }
    }
}