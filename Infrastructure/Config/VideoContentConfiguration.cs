// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Core.Entities.Configuration;
// public class VideoContentConfiguration : IEntityTypeConfiguration<VideoContent>
// {
//     public void Configure(EntityTypeBuilder<VideoContent> builder)
//     {        
//         builder.HasKey(e => e.VideoId);
//
//         builder.ToTable("Video_Contents");
//
//         builder.Property(e => e.VideoId).HasColumnName("VideoID");
//         builder.Property(e => e.SectionId).HasColumnName("ContentID");
//         builder.Property(e => e.VideoUrl)
//             .HasColumnType("VARCHAR(255)")
//             .HasColumnName("VideoURL");
//
//         builder.HasOne(d => d.Section).WithMany(p => p.VideoContents).HasForeignKey(d => d.SectionId);
//     }
// }
