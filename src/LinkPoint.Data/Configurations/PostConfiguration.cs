using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.LikeCount).IsRequired();
        builder.Property(p => p.UserId).HasMaxLength(450);
        builder.Property(p => p.Text).HasMaxLength(300);

        builder.HasOne(p => p.Image)
            .WithOne(i => i.Post)
            .HasForeignKey<Image>(i => i.PostId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.Video)
            .WithOne(v => v.Post)
            .HasForeignKey<Video>(v => v.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p=>p.Likes).WithOne(l=>l.Post).HasForeignKey(l=>l.PostId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(p=>p.Comments).WithOne(c=>c.Post).HasForeignKey(c=>c.PostId).OnDelete(DeleteBehavior.NoAction);
    }
}
