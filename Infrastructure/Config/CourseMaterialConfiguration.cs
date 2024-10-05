using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CourseMaterialConfiguration : IEntityTypeConfiguration<CourseMaterial>
{
    public void Configure(EntityTypeBuilder<CourseMaterial> builder)
    {
                
            // course material is the external links
                builder.HasKey(e => e.MaterialId);

                builder.ToTable("Course_Materials");

                builder.Property(e => e.MaterialId).HasColumnName("MaterialID");
                builder.Property(e => e.CourseId).HasColumnName("CourseID");
                builder.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("DATETIME");
                builder.Property(e => e.FilePath).HasColumnType("VARCHAR(255)");
                builder.Property(e => e.FileType).HasColumnType("VARCHAR(50)");

                builder.HasOne(d => d.Course).WithMany(p => p.CourseMaterials).HasForeignKey(d => d.CourseId);
            
    }
}
