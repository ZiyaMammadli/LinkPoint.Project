using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class UserEducationConfiguration : IEntityTypeConfiguration<UserEducation>
{
    public void Configure(EntityTypeBuilder<UserEducation> builder)
    {
        builder.Property(ue => ue.UserId).HasMaxLength(450);
        builder.Property(ue => ue.University).HasMaxLength(50);
        builder.Property(ue => ue.Description).HasMaxLength(300);
    }
}
