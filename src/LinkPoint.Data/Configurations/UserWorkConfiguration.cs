using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class UserWorkConfiguration : IEntityTypeConfiguration<UserWork>
{
    public void Configure(EntityTypeBuilder<UserWork> builder)
    {
        builder.Property(uw => uw.Company).HasMaxLength(40);
        builder.Property(uw => uw.UserId).HasMaxLength(450);
        builder.Property(uw => uw.Description).HasMaxLength(300);
        builder.Property(uw => uw.Designation).HasMaxLength(40);
    }
}
