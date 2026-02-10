using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Persistence.Configurations;

internal class PurchaseOrderConfigurations : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.Property(p => p.CustomerId)
            .IsRequired();
        builder.Property(p => p.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.ShippingSlipData)
            .HasColumnType("TEXT")
            .IsRequired(false);

        builder.HasOne<CustomerAccount>()
            .WithMany()
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Items)
               .WithOne()
               .HasForeignKey("PurchaseOrderId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
