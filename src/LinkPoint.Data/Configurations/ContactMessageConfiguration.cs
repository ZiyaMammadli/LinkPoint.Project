using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.Property(cm=>cm.Id).IsRequired();
        builder.Property(cm=>cm.UserId).IsRequired();
        builder.Property(cm=>cm.Name).IsRequired().HasMaxLength(50);
        builder.Property(cm=>cm.Email).IsRequired().HasMaxLength(50);
        builder.Property(cm=>cm.PhoneNumber).HasMaxLength(20);
        builder.Property(cm=>cm.Message).IsRequired().HasMaxLength(350);
    }
}
