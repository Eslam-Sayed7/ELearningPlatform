using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CoursesConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {        
            // Property Configurations
            builder.HasKey(c => c.CourseId);
            builder.Property(e => e.CourseId).HasColumnName("CourseID");
            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.CourseName).HasColumnType("VARCHAR(255)");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");
            builder.Property(e => e.Language).HasColumnType("VARCHAR(100)");
            builder.Property(e => e.Level).HasColumnType("VARCHAR(50)");
            builder.Property(e => e.Price)
                .HasDefaultValueSql("0.00")
                .HasColumnType("DOUBLE(10,2)");
            builder.Property(e => e.ThumbnailUrl)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("ThumbnailURL");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            // Relationships
            builder.HasOne(d => d.Category).WithMany(p => p.Courses).HasForeignKey(d => d.CategoryId);
 
            builder.HasOne(c => c.Category)
                .WithMany(e => e.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring many-to-many relationship between instructors and Courses
            builder.HasMany(e => e.Instructors)
                .WithMany(e => e.CoursesTeach)
                .UsingEntity<InstructorsToCourse>(); 
 
    }
}




