using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CourseContentConfiguration : IEntityTypeConfiguration<CourseContent>
{
    public void Configure(EntityTypeBuilder<CourseContent> builder)
    {        
        builder.HasKey(e => e.ContentId);

        builder.ToTable("Course_Content");

        builder.Property(e => e.ContentId).HasColumnName("ContentID");
        builder.Property(e => e.ContentType).HasColumnType("VARCHAR(50)");
        builder.Property(e => e.CourseId).HasColumnName("CourseID");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("DATETIME");
        builder.Property(e => e.Title).HasColumnType("VARCHAR(255)");

        builder.HasOne(d => d.Course).WithMany(p => p.CourseContents).HasForeignKey(d => d.CourseId);

    }
}
