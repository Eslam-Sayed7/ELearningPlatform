// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Core.Entities.Configuration;
// public class TextContentConfiguration : IEntityTypeConfiguration<TextContent>
// {
//     public void Configure(EntityTypeBuilder<TextContent> builder)
//     {
//         
//         builder.HasKey(e => e.TextId);
//
//         builder.ToTable("TextContents");
//         builder.Property(e => e.Body).HasColumnName("Body").HasColumnType("TEXT");
//         builder.Property(e => e.TextId).HasColumnName("TextID");
//         builder.Property(e => e.SectionId).HasColumnName("ContentID");
//
//         builder.HasOne(d => d.Section).WithMany(p => p.TextContents).HasForeignKey(d => d.SectionId);
//             
//     }
// }
