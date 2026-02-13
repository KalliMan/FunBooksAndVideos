using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public sealed class CreatePurchaseOrderResult
{
    private readonly PurchaseOrder _purchaseOrder;

    public CreatePurchaseOrderResult(PurchaseOrder purchaseOrder)
    {
        _purchaseOrder = purchaseOrder;
    }

    public int Id => _purchaseOrder.Id;
}
