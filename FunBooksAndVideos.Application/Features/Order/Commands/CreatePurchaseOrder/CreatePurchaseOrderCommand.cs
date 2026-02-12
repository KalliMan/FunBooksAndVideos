using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Domain;
namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public record CreatePurchaseOrderCommand : ITransactionalCommand<PurchaseOrder>
{
    public int CustomerId { get; set; }

    public IEnumerable<CreatePurchaseOrderItem> Items { get; set; } = new List<CreatePurchaseOrderItem>();
}
