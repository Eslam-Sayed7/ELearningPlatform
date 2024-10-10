using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> builder)
        {
            builder.HasKey(p => p.ProgressId);
            
            builder.Property(p => p.ProgressId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Enrollment)
                .WithOne(e => e.Progress)
                .HasForeignKey<Progress>(p => p.EnrollmentId);

                
            builder.HasOne(p => p.Material)
                   .WithMany()
                   .HasForeignKey(p => p.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);  // Optional, since MaterialId can be null

            builder.Property(p => p.IsCompleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.ToTable("Progresses");
        }
    }
}
