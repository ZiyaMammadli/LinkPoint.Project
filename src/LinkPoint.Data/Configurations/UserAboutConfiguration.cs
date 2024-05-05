using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class UserAboutConfiguration : IEntityTypeConfiguration<UserAbout>
{
    public void Configure(EntityTypeBuilder<UserAbout> builder)
    {
        builder.Property(ua => ua.Male).IsRequired();
        builder.Property(ua => ua.Female).IsRequired();
        builder.Property(ua => ua.UserId).HasMaxLength(450);
        builder.Property(ua => ua.CityName).HasMaxLength(40);
        builder.Property(ua => ua.AboutMe).HasMaxLength(300);
        builder.Property(ua => ua.CountryName).HasMaxLength(40);
    }
}
