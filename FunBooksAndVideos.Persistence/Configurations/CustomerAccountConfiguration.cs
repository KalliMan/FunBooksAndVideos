using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunBooksAndVideos.Persistence.Configurations;

public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
{
    public void Configure(EntityTypeBuilder<CustomerAccount> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.MembershipType)
            .IsRequired()
            .HasConversion<int>() // store enum as int
            .HasDefaultValue(MembershipType.None);
    }
}
