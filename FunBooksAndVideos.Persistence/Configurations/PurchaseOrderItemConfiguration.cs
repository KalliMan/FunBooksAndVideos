using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.Configurations;

public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
    {
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.ItemLineType)
            .IsRequired()
            .HasConversion<int>(); // Store enum as int

        builder.Property(c => c.MembershipType)
            .IsRequired(false)      // MembershipType is nullable, so not required
            .HasConversion<int?>(); // Store enum as int
    }
}
