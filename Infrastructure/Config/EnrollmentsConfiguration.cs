using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;
public class EnrollmentsConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        
        builder.Property(e => e.CourseId).IsRequired();
        builder.Property(e => e.EnrollmentDate);
        builder.Property(e => e.ProgressPercentage).IsRequired(false).HasDefaultValue(0.0); 
        builder.Property(e => e.StudentId).IsRequired();
        // builder.Property(e => e.PaymentId).IsRequired();
        
       
      builder.HasKey(e => e.EnrollmentId);

        // Relationships
        builder.HasOne(e => e.Student)
            .WithMany(u => u.Enrollments)   
            .HasForeignKey(e => e.StudentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(e => e.Course)
            .WithMany(e => e.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

           
        // builder.HasOne(e => e.Payment)
        //     .WithMany(e => e.Enrollments)
        //     .HasForeignKey(p => p.PaymentId)
        //     .OnDelete(DeleteBehavior.NoAction)
        //     .IsRequired();
    }
}



