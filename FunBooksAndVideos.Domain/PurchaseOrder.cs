using FunBooksAndVideos.Domain.Common;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Domain.Exceptions;

namespace FunBooksAndVideos.Domain;

public class PurchaseOrder: BaseEntity
{
    public int CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string? ShippingSlipData { get; private set; }
    
    private readonly List<PurchaseOrderItem> _items = new();
    public IReadOnlyCollection<PurchaseOrderItem> Items => _items.AsReadOnly();

    public PurchaseOrder()
    {        
    }

    public PurchaseOrder(int customerId)
    {
        if (customerId <= 0)
            throw new PurchaseOrderInvariantViolationException("Invalid customer ID");

        CustomerId = customerId;
    }

    public void AddItems(ICollection<PurchaseOrderItem> items)
    {
        if (items == null || items.Count == 0)
        {
            throw new PurchaseOrderInvariantViolationException("Items cannot be null or empty");
        }

        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void AddItem(PurchaseOrderItem item)
    {
        if (item == null)
        {
            throw new PurchaseOrderInvariantViolationException("Item cannot be null");
        }

        _items.Add(item);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Price);
    }

    public void SetShippingSlipData(string? shippingSlipJson)
    {
        ShippingSlipData = shippingSlipJson;
    }

    public bool ContainsMembership() =>
        _items.Any(i => i.ItemLineType == ItemLineType.Membership);
}
