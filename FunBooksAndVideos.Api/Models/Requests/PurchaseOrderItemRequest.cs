using FunBooksAndVideos.Domain.Enums;

namespace FunBooksAndVideos.Api.Models.Requests;

public record PurchaseOrderItemRequest
{
    public int? ProductId { get; set; }
    public ItemLineType ItemLineType { get; set; }
    public MembershipType? MembershipType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
