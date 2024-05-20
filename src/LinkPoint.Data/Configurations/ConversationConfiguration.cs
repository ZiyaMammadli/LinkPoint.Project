using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkPoint.Data.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.Property(conv => conv.User1Id).IsRequired();
        builder.Property(conv=>conv.User2Id).IsRequired();

        builder
            .HasMany(conv=>conv.messages)
            .WithOne(m=>m.Conversation)
            .HasForeignKey(m=>m.ConversationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
