using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class FriendShipConfiguration : IEntityTypeConfiguration<FriendShip>
{
    public void Configure(EntityTypeBuilder<FriendShip> builder)
    {
        builder.Property(fs => fs.UserId).HasMaxLength(450);
        builder.Property(fs => fs.FollowingUserId).HasMaxLength(450);
    }
}
