// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Core.Entities.Configuration;
// public class CourseMaterialConfiguration : IEntityTypeConfiguration<CourseMaterial>
// {
//     public void Configure(EntityTypeBuilder<CourseMaterial> builder)
//     {
//         
//         // course material is the external links for websites / PDFs / articles
//         builder.HasKey(e => e.MaterialId);
//
//
//         builder.Property(e => e.MaterialId).HasColumnName("MaterialID");
//         builder.Property(e => e.SectionId).HasColumnName("SectionID");
//         builder.Property(e => e.MaterialType).HasConversion<int>()  // Ensures enum is stored as integer
//             .HasColumnType("INT");
//         builder.Property(e => e.TextContent).HasColumnType("NVARCHAR(255)");
//         builder.Property(e => e.Url).HasColumnName("Url");
//         builder.Property(e => e.MaterialSequence).HasConversion<int>();
//         
//         builder.HasOne(d => d.Section).WithMany(p => p.CourseMaterials).HasForeignKey(d => d.SectionId);
//             
//         builder.ToTable("CourseMaterials");
//     }
// }
