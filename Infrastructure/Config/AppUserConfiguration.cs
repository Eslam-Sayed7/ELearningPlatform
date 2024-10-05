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
        builder.Property(u => u.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(u => u.RefreshToken).HasMaxLength(255).IsRequired(false);
    }
            
}
