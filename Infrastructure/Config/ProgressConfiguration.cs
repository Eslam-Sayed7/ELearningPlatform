using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> builder)
        {
            // Define composite key
            builder.HasKey(p => new { p.EnrollmentId, p.SectionId });

            // Configure the relationship with Enrollment
            builder.HasOne(p => p.Enrollment)
                .WithMany(e => e.Progresses)
                .HasForeignKey(p => p.EnrollmentId);

            // Configure the relationship with CourseSection
            builder.HasOne(p => p.Section)
                .WithMany()
                .HasForeignKey(p => p.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(p => p.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.ToTable("Progresses");
        }
    }
}