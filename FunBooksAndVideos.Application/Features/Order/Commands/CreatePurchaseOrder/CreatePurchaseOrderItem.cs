using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderItem
{
    public ItemLineType ItemLineType { get; set; }
    public int? ProductId { get; set; }
    public MembershipType? MembershipType { get; set; }
}
