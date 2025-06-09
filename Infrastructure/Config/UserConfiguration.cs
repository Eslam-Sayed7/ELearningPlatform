// using Core.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// namespace Core.Entities.Configuration;
// public class UserConfiguration : IEntityTypeConfiguration<User>
// {
//     public void Configure(EntityTypeBuilder<User> builder)
//     {
//         builder.HasKey(u => u.UserId);
//         
//         // // Property Configurations
//         builder.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
//
//         builder.Property(e => e.UserId).HasColumnName("UserID");
//         builder.Property(e => e.CreatedAt)
//             .HasDefaultValueSql("CURRENT_TIMESTAMP")
//             .HasColumnType("DATETIME");
//         builder.Property(e => e.Email).HasColumnType("VARCHAR(255)");
//         builder.Property(e => e.FirstName).HasColumnType("VARCHAR(255)");
//         builder.Property(e => e.LastName).HasColumnType("VARCHAR(255)");
//         builder.Property(e => e.PasswordHash).HasColumnType("VARCHAR(255)");
//         builder.Property(e => e.ProfilePicture).HasColumnType("VARCHAR(255)");
//         builder.Property(e => e.Role).HasColumnType("VARCHAR(50)");
//     
//
//     }
//             
// }
