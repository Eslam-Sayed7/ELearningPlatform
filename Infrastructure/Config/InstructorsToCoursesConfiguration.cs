using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class InstructorsToCoursesConfiguration : IEntityTypeConfiguration<InstructorsToCourse>
{
    public void Configure(EntityTypeBuilder<InstructorsToCourse> builder)
    {
       builder.HasKey(e => new { e.InstructorToCourseId});
       // builder.HasIndex(e => new{ e.InstructorToCourseId});
       builder.Property(e => e.InstructorId).IsRequired();
       builder.Property(e => e.CourseId).IsRequired();
    }   
}
