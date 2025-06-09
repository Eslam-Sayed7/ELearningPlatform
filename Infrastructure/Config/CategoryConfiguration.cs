using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities.Configuration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        
        builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
        builder.Property(e => e.CategoryName).HasColumnType("VARCHAR(255)");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("DATETIME");
        builder.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

        builder.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory).HasForeignKey(d => d.ParentCategoryId);
    
    }
}
