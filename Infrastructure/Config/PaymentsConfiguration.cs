// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Core.Entities.Configuration;
// public class PaymensConfiguration : IEntityTypeConfiguration<Payment>
// {
//     public void Configure(EntityTypeBuilder<Payment> builder)
//     {
//         
//         // Primary key
//         builder.HasKey(p => p.PaymentId);
//
//         // Properties
//         builder.Property(p => p.StudentId).IsRequired();
//         builder.Property(p => p.CourseId).IsRequired();
//         builder.Property(p => p.Amount)
//                 .IsRequired()
//                 .HasMaxLength(50);  // Assuming you want to limit the length of the amount
//         builder.Property(p => p.paymentStatus)
//                 .IsRequired(false)
//                 .HasMaxLength(50);  // Nullable
//         builder.Property(p => p.TransactionDate).IsRequired();  // Nullable
//         builder.Property(p => p.Discount).IsRequired(false);    // Nullable
//
//         // Relationships
//         builder.HasMany(p => p.Enrollments)
//                 .WithOne(e => e.Payment)
//                 .HasForeignKey(e => e.PaymentId)
//                 .OnDelete(DeleteBehavior.Cascade);  // No cascade delete for related enrollments
//         
//     }
// }
