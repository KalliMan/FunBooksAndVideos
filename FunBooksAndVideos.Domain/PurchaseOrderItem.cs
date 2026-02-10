using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Domain.Exceptions;

namespace FunBooksAndVideos.Domain;
public class PurchaseOrderItem: BaseEntity
{
    public int? ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public ItemLineType ItemLineType { get; private set; }
    public MembershipType? MembershipType { get; private set; }

    private PurchaseOrderItem() { } // EF Core

    public PurchaseOrderItem(ItemLineType type, string name, decimal price, MembershipType membershipType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new PurchaseOrderInvariantViolationException("Name cannot be empty");
        }

        if (price < 0)
        {
            throw new PurchaseOrderInvariantViolationException("Price cannot be negative");
        }

        ItemLineType = type;
        Name = name;
        Price = price;
        MembershipType = membershipType;
    }

        public PurchaseOrderItem(ItemLineType type, string name, decimal price, int productId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new PurchaseOrderInvariantViolationException("Name cannot be empty");
        }

        if (price < 0)
        {
            throw new PurchaseOrderInvariantViolationException("Price cannot be negative");
        }

        ItemLineType = type;
        Name = name;
        Price = price;
        ProductId = productId;
    }
}
