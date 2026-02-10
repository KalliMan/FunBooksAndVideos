using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Purchase.Processing;

public class PurchaseOrderProcessingContext
{
    public PurchaseOrder PurchaseOrder { get; }

    public int CustomerId => PurchaseOrder.CustomerId;

    public IReadOnlyCollection<PurchaseOrderItem> PhysicalItems =>
        PurchaseOrder.Items.Where(i => i.ItemLineType != Domain.Enums.ItemLineType.Membership).ToList().AsReadOnly();
    
    public IReadOnlyCollection<PurchaseOrderItem> MembershipItems =>
        PurchaseOrder.Items.Where(i => i.ItemLineType == Domain.Enums.ItemLineType.Membership).ToList().AsReadOnly();    

    public PurchaseOrderProcessingContext(PurchaseOrder purchaseOrder)
    {
        PurchaseOrder = purchaseOrder;
    }
}
