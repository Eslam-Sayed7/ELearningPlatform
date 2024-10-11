using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CourseSectionConfiguration : IEntityTypeConfiguration<CourseSection>
{
    public void Configure(EntityTypeBuilder<CourseSection> builder)
    {        
        builder.HasKey(e => e.SectionId);


        builder.Property(e => e.SectionId).HasColumnName("SectionId");
        builder.Property(e => e.CourseId).HasColumnName("CourseID");
        builder.Property(e => e.Title).HasColumnType("NVARCHAR(255)");
        builder.Property(e => e.SectionSequence).HasColumnType("INT").HasDefaultValue(0);
        builder.Property(e => e.Link).HasColumnType("NVARCHAR(255)");
        builder.HasOne(d => d.Course)
            .WithMany(p => p.CourseSections)
            .HasForeignKey(d => d.CourseId)
            .IsRequired();
        
        
        builder.ToTable("CoursesSections");
    }
}
