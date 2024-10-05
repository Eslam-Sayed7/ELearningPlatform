// using Core.Entities;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;

// namespace Infrastructure.Identity;
// public class AppIdentityDbContext : IdentityDbContext<AppUser>
// {
//     public AppIdentityDbContext(DbContextOptions options) : base(options) 
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//     }
// }

// public class AppDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
// {
//     public AppIdentityDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();

//         // Use your connection string here
//         optionsBuilder.UseSqlite("Data Source = ELearningPlatform.db");

//         return new AppIdentityDbContext(optionsBuilder.Options);
//     }
// }

