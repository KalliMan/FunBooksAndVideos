using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunBooksAndVideos.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.ItemLineType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.MembershipType)
            .HasConversion<int?>();

        // Seed data
        builder.HasData(
            // Sample Books.
            new
            {
                Id = 1,
                Name = "Dune",
                Description = "Frank Herbert's masterpiece about desert planet Arrakis and the spice melange.",
                Price = 14.99m,
                ItemLineType = ItemLineType.Book,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = 2,
                Name = "Foundation",
                Description = "Isaac Asimov's epic of galactic empire collapse and rebirth through psychohistory.",
                Price = 12.99m,
                ItemLineType = ItemLineType.Book,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = 3,
                Name = "2001: A Space Odyssey",
                Description = "Stanley Kubrick's epic journey from prehistory to beyond Jupiter.",
                Price = 16.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Videos
            new
            {
                Id = 4,
                Name = "The Matrix",
                Description = "The Wachowskis' groundbreaking film about simulated reality and human resistance.",
                Price = 14.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = 5,
                Name = "Interstellar",
                Description = "Christopher Nolan's epic about time dilation and saving humanity through wormholes.",
                Price = 18.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },       
            new
            {
                Id = 6,
                Name = "Dune (2021)",
                Description = "Denis Villeneuve's epic adaptation of Frank Herbert's classic novel.",
                Price = 19.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = 7,
                Name = "Alien",
                Description = "Ridley Scott's horror in space with the iconic Xenomorph.",
                Price = 17.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = 8,
                Name = "The Martian",
                Description = "Ridley Scott's survival story of an astronaut stranded on Mars.",
                Price = 16.99m,
                ItemLineType = ItemLineType.Video,
                MembershipType = (MembershipType?)null,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
