using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(40);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(40);
        builder.Property(u => u.RefreshToken).IsRequired().HasMaxLength(450);

        builder
            .HasOne(u => u.UserAbout)
            .WithOne(ua => ua.User)
            .HasForeignKey<UserAbout>(ua=>ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(u => u.UserWork)
            .WithOne(uw => uw.User)
            .HasForeignKey<UserWork>(uw => uw.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(u => u.UserEducation)
            .WithOne(ue => ue.User)
            .HasForeignKey<UserEducation>(ue => ue.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(u=>u.Posts)
            .WithOne(p=>p.User)
            .HasForeignKey(p=>p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(u=>u.UserInterests)
            .WithOne(ui=>ui.User)
            .HasForeignKey(ui=>ui.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(u=>u.FollowingFriendships)
            .WithOne(fs=>fs.FollowingUser)
            .HasForeignKey(fs => fs.FollowingUserId)
            .OnDelete(DeleteBehavior.NoAction);  
        builder
            .HasMany(u=>u.Friendships)
            .WithOne(fs=>fs.User)
            .HasForeignKey(fs=>fs.UserId)
            .OnDelete(DeleteBehavior.NoAction);  
        builder
            .HasMany(u=>u.likes)
            .WithOne(l=>l.User)
            .HasForeignKey(l=>l.UserId)
            .OnDelete(DeleteBehavior.Cascade);  
        builder
            .HasMany(u=>u.comments)
            .WithOne(c=>c.User)
            .HasForeignKey(c=>c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  
        builder
            .HasMany(u=>u.Images)
            .WithOne(i=>i.User)
            .HasForeignKey(i=>i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
    