using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.ConversationId).IsRequired();
        builder.Property(m=>m.UserId).IsRequired();
        builder.Property(m=>m.Content).IsRequired().HasMaxLength(240);
    }
}
