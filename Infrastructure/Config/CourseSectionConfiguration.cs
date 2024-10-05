using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CourseSectionConfiguration : IEntityTypeConfiguration<CourseSection>
{
    public void Configure(EntityTypeBuilder<CourseSection> builder)
    {        
        builder.HasKey(e => e.ContentId);

        builder.ToTable("Course_Content");

        builder.Property(e => e.ContentId).HasColumnName("ContentID");
        builder.Property(e => e.CourseId).HasColumnName("CourseID");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("DATETIME");
        builder.Property(e => e.Title).HasColumnType("VARCHAR(255)");

        builder.HasOne(d => d.Course).WithMany(p => p.CourseSections).HasForeignKey(d => d.CourseId);

    }
}
