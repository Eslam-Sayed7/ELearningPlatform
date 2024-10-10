using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Entities;
using Core.Entities.Configuration;
using Infrastructure.Config;
using Infrastructure.Configs;
using Microsoft.AspNetCore.Identity;
using ProgressConfiguration = Core.Entities.Configuration.ProgressConfiguration;

namespace Infrastructure.Data;

public partial class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Instructor> Instructors { get; set; }
    public virtual DbSet<CourseSection> CourseSections { get; set; }
    public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Progress> Progresses { get; set; }
    // public virtual DbSet<TextContent> TextContents { get; set; }
    // public virtual DbSet<VideoContent> VideoContents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=../ELearningPlatform.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorsToCoursesConfiguration());
        modelBuilder.ApplyConfiguration(new CoursesConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentsConfiguration());
        modelBuilder.ApplyConfiguration(new CourseSectionConfiguration());
        modelBuilder.ApplyConfiguration(new CourseMaterialConfiguration());
        modelBuilder.ApplyConfiguration(new PaymensConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentsConfiguration());
        modelBuilder.ApplyConfiguration(new ProgressConfiguration());
        
        // modelBuilder.ApplyConfiguration(new TextContentConfiguration());
        // modelBuilder.ApplyConfiguration(new VideoContentConfiguration());

         // Seed Roles
         modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
             new IdentityRole { Name = "Instructor", NormalizedName = "Instructor" },
             new IdentityRole { Name = "Student", NormalizedName = "STUDENT" }
         );
       
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
