using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public record CreatePurchaseOrderCommand : IRequest<int>
{
    public int CustomerId { get; set; }

    public IEnumerable<CreatePurchaseOrderItem> Items { get; set; } = new List<CreatePurchaseOrderItem>();
}
