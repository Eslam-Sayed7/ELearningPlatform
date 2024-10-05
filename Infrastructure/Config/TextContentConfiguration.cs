using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class TextContentConfiguration : IEntityTypeConfiguration<TextContent>
{
    public void Configure(EntityTypeBuilder<TextContent> builder)
    {
        
        builder.HasKey(e => e.TextId);

        builder.ToTable("Text_Contents");

        builder.Property(e => e.TextId).HasColumnName("TextID");
        builder.Property(e => e.ContentId).HasColumnName("ContentID");

        builder.HasOne(d => d.Content).WithMany(p => p.TextContents).HasForeignKey(d => d.ContentId);
            
    }
}
