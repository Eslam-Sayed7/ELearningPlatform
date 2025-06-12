using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Property Configurations
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnType("timestamp").IsRequired();
        builder.Property(u => u.Token).HasColumnType("text").IsRequired(false);
    }
            
}
