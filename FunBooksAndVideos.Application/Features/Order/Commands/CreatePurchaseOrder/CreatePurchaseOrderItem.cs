using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderItem
{
    public int? ProductId { get; set; }
    public ItemLineType ItemLineType { get; set; }
    public MembershipType? MembershipType { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
