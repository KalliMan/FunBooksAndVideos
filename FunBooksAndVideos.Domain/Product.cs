using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Domain.Exceptions;

namespace FunBooksAndVideos.Domain;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public ItemLineType ItemLineType { get; private set; }
    public MembershipType? MembershipType { get; private set; }

    private Product() { } // EF Core

    public Product(string name, string description, decimal price, ItemLineType itemLineType, MembershipType? membershipType = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new PurchaseOrderInvariantViolationException("Product name cannot be empty");
        }

        if (price < 0)
        {
            throw new PurchaseOrderInvariantViolationException("Product price cannot be negative");
        }

        if (itemLineType == ItemLineType.Membership && membershipType == null)
        {
            throw new PurchaseOrderInvariantViolationException("Membership products must have a membership type");
        }

        if (itemLineType != ItemLineType.Membership && membershipType != null)
        {
            throw new PurchaseOrderInvariantViolationException("Only membership products can have a membership type");
        }

        Name = name;
        Description = description;
        Price = price;
        ItemLineType = itemLineType;
        MembershipType = membershipType;
    }
}
