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

        builder.Property(e => e.CourseName).HasColumnType("text");
        builder.Property(e => e.Language).HasColumnType("text");
        builder.Property(e => e.Level).HasColumnType("text");

        builder.Property(e => e.Price)
            .HasDefaultValueSql("0.00")
            .HasColumnType("decimal(10,2)");

        builder.Property(e => e.ThumbnailUrl)
            .HasColumnType("text")
            .HasColumnName("ThumbnailURL");

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .HasColumnType("timestamp");

        builder.Property(e => e.UpdatedAt)
            .HasDefaultValueSql("NOW()")
            .HasColumnType("timestamp");

        // Relationships
        builder.HasOne(d => d.Category)
            .WithMany(p => p.Courses)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuring many-to-many relationship between instructors and courses
        builder.HasMany(e => e.Instructors)
            .WithMany(e => e.CoursesTeach)
            .UsingEntity<InstructorsToCourse>(
                j => j.HasKey(i => new { i.InstructorId, i.CourseId })
            );
    }
}
