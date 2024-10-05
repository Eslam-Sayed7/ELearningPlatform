using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();
            
        builder.HasOne(i => i.Appuser)
            .WithOne()
            .HasForeignKey<Instructor>(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.Expertise).HasMaxLength(100);
        builder.Property(i => i.Bio).HasMaxLength(1000);

        builder.HasMany(i => i.CoursesTeach)
            .WithMany(c => c.Instructors)
            .UsingEntity<InstructorsToCourse>();
    }
}